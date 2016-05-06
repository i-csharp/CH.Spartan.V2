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
using CH.Spartan.Infrastructure;
using CH.Spartan.Notifications;

namespace CH.Spartan.Events
{
    public class EventHandler :
        ISingletonDependency,
        IEventHandler<EntityUpdatedEventData<Device>>,
        IEventHandler<EntityDeletedEventData<Device>>,
        IEventHandler<GetwayEventData>,
        IEventHandler<WebEventData>
    {
        private readonly ActiveMqSendWebEventWorker _activeMqSendWebEventWorker;
        private readonly AlarmNotificationManager _alarmNotificationManager;

        public EventHandler(ActiveMqSendWebEventWorker activeMqSendWebEventWorker)
        {
            _activeMqSendWebEventWorker = activeMqSendWebEventWorker;
        }

        public void HandleEvent(EntityUpdatedEventData<Device> eventData)
        {

        }

        public void HandleEvent(EntityDeletedEventData<Device> eventData)
        {

        }

        public void HandleEvent(GetwayEventData eventData)
        {
            switch (eventData.DataType)
            {
                case EnumGetwayEventDataType.AlarmNotificationData:
                    _alarmNotificationManager.SendAsync(eventData.Data as AlarmNotificationData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void HandleEvent(WebEventData eventData)
        {
            switch (eventData.DataType)
            {
                case EnumWebEventDataType.InstructionData:
                    break;
                case EnumWebEventDataType.DeviceUpdated:
                    break;
                case EnumWebEventDataType.UserSettingUpdated:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
