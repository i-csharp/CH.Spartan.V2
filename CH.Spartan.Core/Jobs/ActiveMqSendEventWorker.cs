using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Infrastructure;
using CH.Spartan.Instructions;
using CH.Spartan.Notifications;

namespace CH.Spartan.Jobs
{
    public class ActiveMqSendEventWorker : ActiveMqSendWorker
    {
        
        public ActiveMqSendEventWorker(ILogger logger, ISettingManager settingManager)
            : base(logger,settingManager)
        {
            
        }

        public override void DoWork(string clientId)
        {
            if (!IsConnected)
            {
                ClientId = clientId;
                Name = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Event_Name);
                Uri = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Event_Uri);
                TryConnect();
            }
        }

    }
}
