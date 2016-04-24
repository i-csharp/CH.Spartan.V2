using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using CH.Spartan.Infrastructure;

namespace CH.Spartan
{

    [DependsOn(typeof(AbpZeroCoreModule))]
    public class SpartanInfrastructureModule : AbpModule
    {
        public override void PreInitialize()
        {
          
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    SpartanConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "CH.Spartan.Infrastructure.Localization.Source"
                        )
                    )
                );
           
            DisableFilterIfHostInterceptorRegister.Initialize(IocManager);
            Configuration.Authorization.Providers.Add<SpartanAuthorizationProvider>();
            Configuration.Settings.Providers.Add<SpartanSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
