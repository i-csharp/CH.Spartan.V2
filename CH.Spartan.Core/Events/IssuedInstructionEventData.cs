using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Events
{
    [Serializable]
    public class IssuedInstructionEventData : EventData
    {
        /// <summary>
        /// 指令类型
        /// </summary>
        public EnumInstructionType Type { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 设备类型Id
        /// </summary>
        public int DeviceTypeId { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
}
