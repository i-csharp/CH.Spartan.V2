using System;
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
using CH.Spartan.Core.Web.Jobs;
using CH.Spartan.Events;
using CH.Spartan.Infrastructure;
using CH.Spartan.Notifications;

namespace CH.Spartan.Core.Web.Events
{
    public class EventHandler :
        ISingletonDependency,
        IEventHandler<EntityUpdatedEventData<Device>>,
        IEventHandler<EntityDeletedEventData<Device>>,
        IEventHandler<UserSettingUpdatedEventData>,
        IEventHandler<IssuedInstructionEventData>
    {
        private readonly ActiveMqSendWebEventWorker _activeMqSendWebEventWorker;
        private readonly AlarmNotificationManager _alarmNotificationManager;

        public EventHandler(ActiveMqSendWebEventWorker activeMqSendWebEventWorker, AlarmNotificationManager alarmNotificationManager)
        {
            _activeMqSendWebEventWorker = activeMqSendWebEventWorker;
            _alarmNotificationManager = alarmNotificationManager;
        }

        public void HandleEvent(EntityUpdatedEventData<Device> eventData)
        {
            _activeMqSendWebEventWorker.SendAsync(new WebEventData
            {
                DataType = EnumWebEventDataType.DeviceUpdated,
                Data = eventData.Entity.Id.ToString(),
                EntityId = eventData.Entity.Id.ToString()
            });
        }

        public void HandleEvent(EntityDeletedEventData<Device> eventData)
        {
            _activeMqSendWebEventWorker.SendAsync(new WebEventData
            {
                DataType = EnumWebEventDataType.DeviceDeleted,
                Data = eventData.Entity.Id.ToString(),
                EntityId = eventData.Entity.Id.ToString()
            });
        }

        public void HandleEvent(UserSettingUpdatedEventData eventData)
        {
            _activeMqSendWebEventWorker.SendAsync(new WebEventData
            {
                DataType = EnumWebEventDataType.UserSettingUpdated,
                Data = eventData.ToJsonString(),
                EntityId = eventData.UserId.ToString()
            });
        }

        public void HandleEvent(IssuedInstructionEventData eventData)
        {
            _activeMqSendWebEventWorker.SendAsync(new WebEventData
            {
                DataType = EnumWebEventDataType.InstructionData,
                Data = eventData.ToJsonString(),
                EntityId = eventData.DeviceId.ToString()
            });
        }

        public void HandleEvent(GetwayEventData eventData)
        {
            switch (eventData.DataType)
            {
                case EnumGetwayEventDataType.AlarmNotificationData:
                    _alarmNotificationManager.Send(eventData.Data.ToObject<AlarmNotificationData>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


      
    }
}
