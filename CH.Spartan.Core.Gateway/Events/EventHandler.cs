﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using CH.Spartan.Devices;
using CH.Spartan.Jobs;
using Abp.Events.Bus.Entities;
using Abp.Json;
using CH.Spartan.Events;
using CH.Spartan.Infrastructure;
using CH.Spartan.Notifications;

namespace CH.Spartan.Core.Gateway.Events
{
    public class EventHandler :
        ISingletonDependency,
        IEventHandler<WebEventData>
    {
        public void HandleEvent(WebEventData eventData)
        {

            //SendAsync(new GetwayEventData
            //{
            //    DataType = EnumGetwayEventDataType.AlarmNotificationData,
            //    EntityId = "1",
            //    Data = new AlarmNotificationData
            //    {
            //        Title = "粤B56325",
            //        UserId = 3,
            //        DeviceId = 1,
            //        AlarmType = EnumAlarmType.Shake,
            //        Content = "震动报警",
            //        Latitude = 23.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
            //        Longitude = 113.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0,
            //        Severity = NotificationSeverity.Warn,
            //        ReportTime = DateTime.Now.AddSeconds(-new Random(Guid.NewGuid().GetHashCode()).Next(100, 200))
            //    }.ToJsonString()
            //});
        }
    }
}
