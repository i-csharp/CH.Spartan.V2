using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Castle.Core.Internal;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.Commons.Dto;
using CH.Spartan.Editions;
using CH.Spartan.MultiTenancy.Dto;
using CH.Spartan.Users;
using System.Linq.Dynamic;
using System.Data.Entity;
using Abp.Extensions;
using CH.Spartan.Commons.Linq;
using System;

namespace CH.Spartan.MultiTenancy
{
    public class TenantAppService : SpartanAppServiceBase, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly RoleManager _roleManager;
        private readonly EditionManager _editionManager;
        private readonly IRepository<Tenant> _tenantRepository; 
        public TenantAppService(TenantManager tenantManager, RoleManager roleManager, EditionManager editionManager, IRepository<Tenant> tenantRepository)
        {
            _tenantManager = tenantManager;
            _roleManager = roleManager;
            _editionManager = editionManager;
            _tenantRepository = tenantRepository;
        }

        public async Task<ListResultOutput<TenantListDto>> GetTenantList(GetTenantListInput input)
        {
            var list = await _tenantManager.Tenants
                .OrderBy(t => t.TenancyName)
                .ToListAsync();

            return new ListResultOutput<TenantListDto>(list.MapTo<List<TenantListDto>>());
        }

        public async Task<PagedResultOutput<TenantListDto>> GetTenantPaged(GetTenantPagedInput input)
        {
            var query = _tenantRepository.GetAll()
                .WhereIf(!input.SearchText.IsNullOrEmpty(),p => p.TenancyName.Contains(input.SearchText) || p.Name.Contains(input.SearchText));

            var count = await query.CountAsync();

            var list = await query.OrderBy(input).PageBy(input).ToListAsync();

            return new PagedResultOutput<TenantListDto>(count, list.MapTo<List<TenantListDto>>());
        }

        private async Task CreateTenant(EditTenantInput input)
        {
            
            //����һ���⻧
            var tenant = new Tenant(input.Tenant.TenancyName, input.Tenant.Name) {IsActive = true};
            //���õ�ǰ�⻧ Ĭ�ϰ汾
            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            CheckErrors(await TenantManager.CreateAsync(tenant));
            await CurrentUnitOfWork.SaveChangesAsync(); //Ŀ����Ϊ�˻�ȡ���⻧�� Id
            Role adminRole;
            Role userRole;
            //���õ�ǰ������ ���ݹ��� (���еĲ�ѯ �������TenantId= tenant.Id �������)
            using (CurrentUnitOfWork.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, tenant.Id))
            {
                //���һ����̬��ɫ(Admin) ���⻧
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync();

                //�����е��⻧Ȩ�޸��������ɫ
                adminRole = _roleManager.Roles.Single(r => r.Name == RoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //����һ�����⻧���û���ɫ ���������ɫ��Ĭ���û�������ʱ�����ӵ�
                await _roleManager.CreateUserRoles(tenant.Id);
                //�����е��⻧���û��Ľ�ɫ����ý�ɫ
                userRole = _roleManager.Roles.Single(r => r.Name == RoleNames.Tenants.User);
                await _roleManager.GrantAllUserPermissionsAsync(userRole);
            }

            //������⻧ ���һ������Ա�û�
            User adminUser;
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                //ȡ���⻧���� Ŀ�� ��Ϊ����������û���ʱ�� ����û�����Ψһ�� (�������⻧)
                adminUser = User.CreateTenantAdminUser(tenant.Id, input.Tenant.TenancyName, input.Tenant.EmailAddress, User.DefaultPassword);
                CheckErrors(await UserManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            //���½����⻧����Ա ���� �ղŴ����Ĺ���Ա��ɫ
            CheckErrors(await UserManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task UpdateTenant(EditTenantInput input)
        {
            var tenant = await _tenantRepository.FirstOrDefaultAsync(input.Tenant.Id);
            input.Tenant.MapTo(tenant);
            await _tenantRepository.UpdateAsync(tenant);
        }

        public async Task EditTenant(EditTenantInput input)
        {
            if (input.Tenant.Id == 0)
            {
                await CreateTenant(input);
            }
            else
            {
                await UpdateTenant(input);
            }
        }

        public async Task<EditTenantOutput> FetchTenant(NullableIdInput input)
        {
            if (input.Id.HasValue)
            {
                return new EditTenantOutput((await _tenantRepository.FirstOrDefaultAsync(input.Id.Value)).MapTo<TenantDto>());
            }
            else
            {
                return new EditTenantOutput(new TenantDto());
            }
        }

        public async Task DeleteTenant(List<IdInput> input)
        {
           await _tenantRepository.DeleteAsync(p=>p.Id.IsIn(input.Select(o=>o.Id).ToArray()));
        }
    }
}