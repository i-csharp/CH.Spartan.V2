using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Zero;
using Abp.Zero.Configuration;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.Infrastructure;
using CH.Spartan.Tenants;
using Microsoft.AspNet.Identity;

namespace CH.Spartan.Users
{
    public class UserManager : AbpUserManager<Tenant, Role, User>
    {
        private readonly IRepository<User,long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UserManager(
            UserStore store,
            RoleManager roleManager,
            IRepository<Tenant> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, IRepository<User, long> userRepository)
            : base(
                store,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                iocResolver,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                userLoginAttemptRepository)
        {
            _userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        private string L(string name)
        {
            return LocalizationManager.GetString(AbpZeroConsts.LocalizationSourceName, name);
        }

        public async Task<string> GetTenancyNameAsync(string userName)
        {
            var user = await Store.FindByNameAsync(userName);
            return user?.Tenant != null ? user.Tenant.TenancyName : "";
        }

        public async Task<bool> CheckIsTenantAdminAsync(long userId)
        {
            return await IsInRoleAsync(userId, StaticRoleNames.Tenants.Admin);
        }

        public async Task DeleteByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var user = _userRepository.Get(id);
                if (user.IsStatic) continue;
                await _userRepository.DeleteAsync(id);
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword1,
            string newPassword2)
        {
            var passwordHasher = new Md532PasswordHasher();
            if (!newPassword1.Equals(newPassword2))
            {
                return AbpIdentityResult.Failed(L("两次输入的新密不一致"));
            }

            if (passwordHasher.VerifyHashedPassword(user.Password, oldPassword) == PasswordVerificationResult.Failed)
            {
                return AbpIdentityResult.Failed(L("密码不正确"));
            }
            await AbpStore.SetPasswordHashAsync(user, passwordHasher.HashPassword(newPassword1));
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> CheckDuplicateUsernameOrEmailAddressAsync(long? expectedUserId, string userName, string emailAddress)
        {
            //全范围内查找 不区分租户
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user = (await FindByNameAsync(userName));
                if (user != null && user.Id != expectedUserId)
                {
                    return AbpIdentityResult.Failed(string.Format(L("Identity.DuplicateName"), userName));
                }

                user = (await FindByEmailAsync(emailAddress));
                if (user != null && user.Id != expectedUserId)
                {
                    return AbpIdentityResult.Failed(string.Format(L("Identity.DuplicateEmail"), emailAddress));
                }
                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> CreateUserAsync(User user)
        {
            user.Surname = user.UserName;
            user.Password = new Md532PasswordHasher().HashPassword(SpartanConsts.DefaultPassword);
            user.IsActive = true;
            user.IsEmailConfirmed = true;
            user.IsStatic = false;
            var result = await CreateAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }

            await _unitOfWorkManager.Current.SaveChangesAsync();
            using (_unitOfWorkManager.Current.SetFilterParameter(AbpDataFilters.MayHaveTenant,AbpDataFilters.Parameters.TenantId, user.TenantId))
            {
                var roles = RoleManager.Roles.Where(p => p.IsDefault).ToList();
                foreach (var role in roles)
                {
                    result = await AddToRoleAsync(user.Id, role.Name);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
            return IdentityResult.Success;
        }
    }
}