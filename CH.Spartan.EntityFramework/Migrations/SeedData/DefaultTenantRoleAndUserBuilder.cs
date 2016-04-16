using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using CH.Spartan.Authorization;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.EntityFramework;
using CH.Spartan.Infrastructure;
using CH.Spartan.MultiTenancy;
using CH.Spartan.Users;
using Microsoft.AspNet.Identity;

namespace CH.Spartan.Migrations.SeedData
{
    public class DefaultTenantRoleAndUserBuilder
    {
        private readonly SpartanDbContext _context;

        public DefaultTenantRoleAndUserBuilder(SpartanDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //��� ��������Ա��ɫ ��̬(�ý�ɫ���������Ȩ��)
            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Admin, DisplayName = StaticRoleNames.Host.Admin, IsStatic = true });
                _context.SaveChanges();
            }

            //�������� ����Ȩ�� ����������Ա��ɫ
            var permissions = PermissionFinder
                .GetAllPermissions(new SpartanAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                .ToList();

            foreach (var permission in permissions)
            {
                if (!permission.IsGrantedByDefault)
                {

                    var permissionSetting =
                        _context.RolePermissions.FirstOrDefault(
                            p => p.Name == permission.Name && p.IsGranted && p.RoleId == adminRoleForHost.Id);

                    if (permissionSetting == null)
                    {
                        _context.Permissions.Add(
                            new RolePermissionSetting
                            {
                                Name = permission.Name,
                                IsGranted = true,
                                RoleId = adminRoleForHost.Id
                            });
                    }
                }
            }
            _context.SaveChanges();

            //���һ������
            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = User.AdminUserName,
                        Name = User.AdminUserName,
                        Surname = "����Ա",
                        EmailAddress = "359875450@qq.com",
                        IsEmailConfirmed = true,
                        Password = new Md532PasswordHasher().HashPassword(SpartanConsts.DefaultPassword)
                    });

                _context.SaveChanges();
                //������ ������������Ա��ɫ
                _context.UserRoles.Add(new UserRole(adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();
            }


            //�������⻧����Ա��ɫ ���Ȩ��
            var tenantAdminRoles =
                _context.Roles.Where(p => p.Name == StaticRoleNames.Tenants.Admin && p.TenantId != null).ToList();

            permissions = PermissionFinder
                .GetAllPermissions(new SpartanAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                .ToList();

            foreach (var tenantAdminRole in tenantAdminRoles)
            {
                foreach (var permission in permissions)
                {
                    if (!permission.IsGrantedByDefault)
                    {

                        var permissionSetting =
                            _context.RolePermissions.FirstOrDefault(
                                p => p.Name == permission.Name && p.IsGranted && p.RoleId == tenantAdminRole.Id);

                        if (permissionSetting == null)
                        {
                            _context.Permissions.Add(
                                new RolePermissionSetting
                                {
                                    Name = permission.Name,
                                    IsGranted = true,
                                    RoleId = tenantAdminRole.Id
                                });
                        }
                    }
                }
                _context.SaveChanges();
            }
























        }
    }
}