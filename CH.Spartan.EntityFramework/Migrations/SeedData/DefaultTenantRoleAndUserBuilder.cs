using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using CH.Spartan.Authorization;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.EntityFramework;
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

                //�������� ����Ȩ�� ����������Ա��ɫ
                var permissions = PermissionFinder
                    .GetAllPermissions(new SpartanAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                foreach (var permission in permissions)
                {
                    if (!permission.IsGrantedByDefault)
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

                _context.SaveChanges();
            }

            //���һ������
            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = "admin",
                        Name = "admin",
                        Surname = "admin",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = new PasswordHasher().HashPassword(User.DefaultPassword)
                    });

                _context.SaveChanges();
                //������ ������������Ա��ɫ
                _context.UserRoles.Add(new UserRole(adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();
            }

            //����⻧
            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == "yugps");
            if (defaultTenant == null)
            {
                defaultTenant = _context.Tenants.Add(new Tenant { TenancyName = "yugps", Name = "����" });
                _context.SaveChanges();
            }

            //����⻧ ����Ա��ɫ ��̬(�ý�ɫ���������Ȩ��)
            var adminRoleForDefaultTenant = _context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRoleForDefaultTenant == null)
            {
                adminRoleForDefaultTenant = _context.Roles.Add(new Role { TenantId = defaultTenant.Id, Name = StaticRoleNames.Tenants.Admin, DisplayName = StaticRoleNames.Tenants.Admin, IsStatic = true });
                _context.SaveChanges();

                var permissions = PermissionFinder
                    .GetAllPermissions(new SpartanAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                foreach (var permission in permissions)
                {
                    if (!permission.IsGrantedByDefault)
                    {
                        _context.Permissions.Add(
                            new RolePermissionSetting
                            {
                                Name = permission.Name,
                                IsGranted = true,
                                RoleId = adminRoleForDefaultTenant.Id
                            });
                    }
                }
                _context.SaveChanges();
            }

            //����⻧ �û�
            var adminUserForDefaultTenant = _context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == User.AdminUserName);
            if (adminUserForDefaultTenant == null)
            {
                adminUserForDefaultTenant = _context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "yugps",
                        Name = "yugps",
                        Surname = "yugps",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = new PasswordHasher().HashPassword(User.DefaultPassword)
                    });
                _context.SaveChanges();
                _context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, adminRoleForDefaultTenant.Id));
                _context.SaveChanges();
            }
        }
    }
}