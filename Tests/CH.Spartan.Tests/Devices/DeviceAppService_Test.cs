using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Spartan.Devices;
using CH.Spartan.Devices.Dto;
using Xunit;

namespace CH.Spartan.Tests.Devices
{
   public class DeviceAppService_Test: SpartanTestBase
    {
        private readonly IDeviceAppService _deviceAppService;

        public DeviceAppService_Test()
        {
            _deviceAppService = Resolve<IDeviceAppService>();
        }

        [Fact]
        public async Task Should_Get_Current_User_When_Logged_In_As_Host()
        {
            LoginAsTenant("yugps", "yugps");
            var result = await _deviceAppService.GetMonitorDataByCutomerForWeb(new GetMonitorDataByCutomerForWebInput());
        }
    }
}
