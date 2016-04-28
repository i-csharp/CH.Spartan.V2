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
            CreateHost();
            CreateTenant("羽衡科技", "yugps", "yugps@qq.com", "18566267191", 100000);
            CreateTenant("哈尔滨卓拓科技", "hebztkj", "hebztkj@qq.com", "18533696989", 2000);
            UpdateTenantUserRolePermission();
            UpdateTenantAdminRolePermission();
        }

        private void CreateHost()
        {
            //添加 租主管理员角色 静态(该角色不允许更改权限)
            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost =
                    _context.Roles.Add(new Role
                    {
                        Name = StaticRoleNames.Host.Admin,
                        DisplayName = StaticRoleNames.Host.Admin,
                        IsStatic = true
                    });
                _context.SaveChanges();
            }

            //分配所有 租主权限 给租主管理员角色
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

            //添加一个租主
            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = User.AdminUserName,
                        Name = "超级管理员",
                        Surname = "超级管理员",
                        EmailAddress = "359875450@qq.com",
                        IsEmailConfirmed = true,
                        IsStatic = true,
                        Password = new Md532PasswordHasher().HashPassword(SpartanConsts.DefaultPassword)
                    });

                _context.SaveChanges();
                //给租主 赋予租主管理员角色
                _context.UserRoles.Add(new UserRole(adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();
            }
        }

        private void CreateTenant(string name, string tenancyName, string emailAddress, string phone, decimal balance)
        {
            //添加一个租户
            var tenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName);
            if (tenant == null)
            {
                tenant = _context.Tenants.Add(new Tenant()
                {
                    Name = name,
                    TenancyName = tenancyName,
                    IsActive = true,
                    Balance = balance,
                    Phone = phone
                });
                _context.SaveChanges();
            }

            //添加 租户管理员角色 静态(该角色不允许更改权限)
            var adminRoleForTenant =
                _context.Roles.FirstOrDefault(r => r.TenantId == tenant.Id && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRoleForTenant == null)
            {
                adminRoleForTenant =
                    _context.Roles.Add(new Role
                    {
                        TenantId = tenant.Id,
                        Name = StaticRoleNames.Tenants.Admin,
                        DisplayName = StaticRoleNames.Tenants.Admin,
                        IsStatic = true
                    });
                _context.SaveChanges();
            }

            //分配所有 租户权限 给租户管理员角色
            var adminPermissionsForTenant = PermissionFinder
                .GetAllPermissions(new SpartanAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                .ToList();

            foreach (var permission in adminPermissionsForTenant)
            {
                if (!permission.IsGrantedByDefault)
                {
                    var permissionSetting =
                        _context.RolePermissions.FirstOrDefault(
                            p => p.Name == permission.Name && p.IsGranted && p.RoleId == adminRoleForTenant.Id);

                    if (permissionSetting == null)
                    {
                        _context.Permissions.Add(
                            new RolePermissionSetting
                            {
                                Name = permission.Name,
                                IsGranted = true,
                                RoleId = adminRoleForTenant.Id
                            });
                    }
                }
            }
            _context.SaveChanges();


            //添加 租户用户角色 静态(该角色不允许更改权限)
            var userRoleForTenant =
                _context.Roles.FirstOrDefault(r => r.TenantId == tenant.Id && r.Name == StaticRoleNames.Tenants.User);
            if (userRoleForTenant == null)
            {
                userRoleForTenant =
                    _context.Roles.Add(new Role
                    {
                        TenantId = tenant.Id,
                        Name = StaticRoleNames.Tenants.User,
                        DisplayName = StaticRoleNames.Tenants.User,
                        IsStatic = true,
                        IsDefault = true
                    });
                _context.SaveChanges();
            }

            //分配部分 租户权限 给租户用户角色
            var userPermissionsForTenant =
                adminPermissionsForTenant.Where(p => !p.Name.StartsWith(SpartanPermissionNames.AgentManages));

            foreach (var permission in userPermissionsForTenant)
            {
                if (!permission.IsGrantedByDefault)
                {
                    var permissionSetting =
                        _context.RolePermissions.FirstOrDefault(
                            p => p.Name == permission.Name && p.IsGranted && p.RoleId == userRoleForTenant.Id);

                    if (permissionSetting == null)
                    {
                        _context.Permissions.Add(
                            new RolePermissionSetting
                            {
                                Name = permission.Name,
                                IsGranted = true,
                                RoleId = userRoleForTenant.Id
                            });
                    }
                }
            }
            _context.SaveChanges();

            //添加一个租户管理员
            var adminUserFoTenant = _context.Users.FirstOrDefault(u => u.TenantId == tenant.Id && u.UserName == tenancyName);
            if (adminUserFoTenant == null)
            {
                adminUserFoTenant = _context.Users.Add(
                    new User
                    {
                        TenantId = tenant.Id,
                        UserName = tenancyName,
                        Name = "管理员",
                        Surname = "管理员",
                        EmailAddress = emailAddress,
                        IsEmailConfirmed = true,
                        IsStatic = true,
                        Password = new Md532PasswordHasher().HashPassword(SpartanConsts.DefaultPassword)
                    });

                _context.SaveChanges();
                //给租主 赋予租户管理员角色
                _context.UserRoles.Add(new UserRole(adminUserFoTenant.Id, adminRoleForTenant.Id));
                _context.SaveChanges();
            }
        }

        private void UpdateTenantUserRolePermission()
        {
            //给所有租户管理员角色 添加权限 更新权限的时候用
            var tenantUserRoles =
                _context.Roles.Where(p => p.Name == StaticRoleNames.Tenants.User && p.TenantId != null).ToList();

            var tenantUserRolePermissions = PermissionFinder
                .GetAllPermissions(new SpartanAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) && !p.Name.StartsWith(SpartanPermissionNames.AgentManages))
                .ToList();

            foreach (var tenantUserRole in tenantUserRoles)
            {
                foreach (var permission in tenantUserRolePermissions)
                {
                    if (!permission.IsGrantedByDefault)
                    {
                        var permissionSetting =
                            _context.RolePermissions.FirstOrDefault(
                                p => p.Name == permission.Name && p.IsGranted && p.RoleId == tenantUserRole.Id);

                        if (permissionSetting == null)
                        {
                            _context.Permissions.Add(
                                new RolePermissionSetting
                                {
                                    Name = permission.Name,
                                    IsGranted = true,
                                    RoleId = tenantUserRole.Id
                                });
                        }
                    }
                }
                _context.SaveChanges();
            }
        }

        private void UpdateTenantAdminRolePermission()
        {
            //给所有租户管理员角色 添加权限 更新权限的时候用
            var tenantAdminRoles =
                _context.Roles.Where(p => p.Name == StaticRoleNames.Tenants.Admin && p.TenantId != null).ToList();

            var tenantAdminRolePermissions = PermissionFinder
                .GetAllPermissions(new SpartanAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                .ToList();

            foreach (var tenantAdminRole in tenantAdminRoles)
            {
                foreach (var permission in tenantAdminRolePermissions)
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