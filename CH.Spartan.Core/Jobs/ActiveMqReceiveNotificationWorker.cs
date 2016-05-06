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
using CH.Spartan.Notifications;

namespace CH.Spartan.Jobs
{
    public class ActiveMqReceiveNotificationWorker : ActiveMqReceiveWorker
    {
        private readonly AlarmNotificationManager _alarmNotificationManager;

        public ActiveMqReceiveNotificationWorker(ILogger logger, ISettingManager settingManager, AlarmNotificationManager alarmNotificationManager) : base(logger,settingManager)
        {
            _alarmNotificationManager = alarmNotificationManager;
        }

        public override void DoWork(string clientId)
        {
            if (!IsConnected)
            {
                ClientId = clientId;
                Name = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Notification_Name);
                Uri = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Notification_Uri);
                TryConnect();
            }
        }

        protected override void Received(IObjectMessage message)
        {
            var data = message?.Body as AlarmNotificationData;
            if (data != null)
            {
               _alarmNotificationManager.SendAsync(data);
            }
        }
    }
}
