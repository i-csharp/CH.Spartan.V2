using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.Zero;
using CH.Spartan.Core.Web.Jobs;
using Hangfire;

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
            RecurringJob.AddOrUpdate<ActiveMqSendWebEventWorker>(p => p.DoWork("Default"), Cron.Minutely);
            RecurringJob.AddOrUpdate<ActiveMqReceiveGetwayEventWorker>(p => p.DoWork("Default"), Cron.Minutely);
        }
    }
}
