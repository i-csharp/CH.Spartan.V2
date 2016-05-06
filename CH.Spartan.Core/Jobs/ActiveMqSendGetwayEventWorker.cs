using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Notifications;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Events;
using CH.Spartan.Infrastructure;
using CH.Spartan.Notifications;

namespace CH.Spartan.Jobs
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
            else
            {
#if DEBUG
                SendAsync(new GetwayEventData
                {
                    DataType = EnumGetwayEventDataType.AlarmNotificationData,
                    EntityId = 1,
                    Data = new AlarmNotificationData
                    {
                        Title = "粤B56325",
                        UserId = 3,
                        DeviceId = 1,
                        AlarmType = EnumAlarmType.Shake,
                        Content = "震动报警",
                        Latitude = 23.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000)/10000.0,
                        Longitude = 113.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000)/10000.0,
                        Severity = NotificationSeverity.Warn,
                        ReportTime = DateTime.Now.AddSeconds(-new Random(Guid.NewGuid().GetHashCode()).Next(100, 200))
                    }
                });
#endif
            }
        }

    }
}
