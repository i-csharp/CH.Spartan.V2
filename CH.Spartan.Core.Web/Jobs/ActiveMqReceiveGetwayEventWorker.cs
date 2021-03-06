﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Threading.Timers;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Events;
using CH.Spartan.Infrastructure;
using CH.Spartan.Jobs;
using CH.Spartan.Notifications;

namespace CH.Spartan.Core.Web.Jobs
{

    public class ActiveMqReceiveGetwayEventWorker : ActiveMqReceiveWorker, ISingletonDependency
    {

        public ActiveMqReceiveGetwayEventWorker(AbpTimer timer, IEventBus eventBus) : base(timer, eventBus)
        {
            ClientId = "Default";
        }

        protected override void DoWork()
        {
            if (!IsConnected)
            {
                Name = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Name);
                Uri = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Uri);
                TryConnect();
            }
        }

        protected override void Received(IObjectMessage message)
        {
            var data = message as GetwayEventData;
            if (data == null)
            {
                return;
            }
            EventBus.TriggerAsync(data);
        }
    }
}
