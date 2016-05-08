using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Abp.Zero;
using Abp.Zero.Configuration;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.Core.Gateway.Jobs;
using CH.Spartan.Jobs;

namespace CH.Spartan.Core.Gateway
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(SpartanCoreModule))]
    public class SpartanCoreGateway:AbpModule
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
            workManager.Add(IocManager.Resolve<ActiveMqReceiveWebEventWorker>());
            workManager.Add(IocManager.Resolve<ActiveMqSendGetwayEventWorker>());
        }
    }
}
