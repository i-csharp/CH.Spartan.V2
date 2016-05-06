using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Json;
using Abp.Notifications;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Events;
using CH.Spartan.Infrastructure;
using CH.Spartan.Jobs;
using CH.Spartan.Notifications;

namespace CH.Spartan.Core.Gateway.Jobs
{
    public class ActiveMqSendGetwayEventWorker : ActiveMqSendWorker
    {
      
        public ActiveMqSendGetwayEventWorker(ILogger logger, ISettingManager settingManager)
            : base(logger, settingManager)
        {
          
        }

        public override void DoWork(string clientId)
        {
            if (!IsConnected)
            {
                ClientId = clientId;
                Name = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Name);
                Uri = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Uri);
                TryConnect();
            }
        }

    }
}
