using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using CH.Spartan.Authorization;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.Infrastructure;

namespace CH.Spartan
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class SpartanCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
