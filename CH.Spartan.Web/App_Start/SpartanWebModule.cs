using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using CH.Spartan.Api;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Web
{
    [DependsOn(
        typeof(SpartanDataModule),
        typeof(SpartanApplicationModule),
        typeof(SpartanWebApiModule),
        typeof(AbpWebSignalRModule),
        typeof(AbpWebMvcModule))]
    public class SpartanWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
