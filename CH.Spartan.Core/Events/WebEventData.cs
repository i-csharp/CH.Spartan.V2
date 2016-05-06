using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Events
{
   public class WebEventData: EventData
    {
        public EnumWebEventDataType DataType { get; set; }

        public string EntityId { get; set; }

        public string Data { get; set; }
    }
}
