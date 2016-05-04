using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using CH.Spartan.Devices;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.DeviceTypes
{
   public static class DeviceTypeHelper
    {
        public static string CreateCode(Device device, DeviceType deviceType)
        {

            switch (deviceType.CodeCreateRule)
            {
                case EnumCodeCreateRule.No:
                    return device.BNo;
                case EnumCodeCreateRule.PrefixZeroAndNo:
                    return $"0{device.BNo}";
                default:
                    return device.BNo;
            }
        }
    }
}
