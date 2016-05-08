using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Abp.Zero;
using CH.Spartan.Core.Web.Jobs;

namespace CH.Spartan.Core.Web
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(SpartanCoreModule))]
    public class SpartanCoreWebModule: AbpModule
    {
        public override void PreInitialize()
        {

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<ActiveMqSendWebEventWorker>());
            workManager.Add(IocManager.Resolve<ActiveMqReceiveGetwayEventWorker>());
        }
    }
}
