using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Threading.Timers;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Devices;
using CH.Spartan.Events;
using CH.Spartan.Infrastructure;
using CH.Spartan.Instructions;
using CH.Spartan.Jobs;
using CH.Spartan.Notifications;

namespace CH.Spartan.Core.Web.Jobs
{
    public class ActiveMqSendWebEventWorker : ActiveMqSendWorker, ISingletonDependency
    {

        public ActiveMqSendWebEventWorker(AbpTimer timer)
            : base(timer)
        {
            ClientId = "Default";
        }

        protected override void DoWork()
        {
            if (!IsConnected)
            {
                Name = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Web_Event_Name);
                Uri = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Web_Event_Uri);
                TryConnect();
            }
        }
    }
}
