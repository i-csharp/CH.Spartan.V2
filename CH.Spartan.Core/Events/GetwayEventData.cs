using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;
using CH.Spartan.Infrastructure;
using CH.Spartan.Notifications;

namespace CH.Spartan.Events
{
    [Serializable]
    public class GetwayEventData : EventData
    {
        public EnumGetwayEventDataType DataType { get; set; }

        public string EntityId { get; set; }

        public string Data { get; set; }
    }
}
