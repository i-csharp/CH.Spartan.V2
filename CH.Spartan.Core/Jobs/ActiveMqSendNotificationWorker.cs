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
using CH.Spartan.Infrastructure;
using CH.Spartan.Notifications;

namespace CH.Spartan.Jobs
{
    public class ActiveMqSendNotificationWorker : ActiveMqSendWorker
    {
      
        public ActiveMqSendNotificationWorker(ILogger logger, ISettingManager settingManager)
            : base(logger, settingManager)
        {
          
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
            else
            {
#if DEBUG
                Send(new AlarmNotificationData()
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
                });

                Send(new AlarmNotificationData()
                {
                    Title = "粤B36598",
                    UserId = 3,
                    DeviceId = 2,
                    AlarmType = EnumAlarmType.OutFortify,
                    Content = "离开设防报警",
                    Latitude = 23.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Longitude = 113.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Severity = NotificationSeverity.Error,
                    ReportTime = DateTime.Now.AddSeconds(-new Random(Guid.NewGuid().GetHashCode()).Next(100, 200))
                });

                Send(new AlarmNotificationData()
                {
                    Title = "粤B36598",
                    UserId = 3,
                    DeviceId = 3,
                    AlarmType = EnumAlarmType.InArea,
                    Content = "进入区域 购物公园",
                    Latitude = 23.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Longitude = 113.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Severity = NotificationSeverity.Info,
                    ReportTime = DateTime.Now.AddSeconds(-new Random(Guid.NewGuid().GetHashCode()).Next(100, 200))
                });

                Send(new AlarmNotificationData()
                {
                    Title = "桂A33333",
                    UserId = 3,
                    DeviceId = 4,
                    AlarmType = EnumAlarmType.OverSpeed,
                    Content = "超速报警 当前速度 85km/h 限速 80km/h",
                    Latitude = 23.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Longitude = 113.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Severity = NotificationSeverity.Fatal,
                    ReportTime = DateTime.Now.AddSeconds(-new Random(Guid.NewGuid().GetHashCode()).Next(100, 200))
                });

                Send(new AlarmNotificationData()
                {
                    Title = "桂A56778",
                    UserId = 3,
                    DeviceId = 5,
                    AlarmType = EnumAlarmType.LowBattery,
                    Content = "低电报警 当前电量 10%",
                    Latitude = 23.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Longitude = 113.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
                    Severity = NotificationSeverity.Warn,
                    ReportTime = DateTime.Now.AddSeconds(-new Random(Guid.NewGuid().GetHashCode()).Next(100, 200))
                });
#endif
            }
        }

    }
}
