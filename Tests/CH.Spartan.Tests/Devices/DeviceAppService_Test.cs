using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using CH.Spartan.Devices;
using CH.Spartan.Devices.Dto;
using Shouldly;
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
        public async Task GetMonitorDataByCutomerForWeb()
        {
            LoginAsTenant("yugps", "yugps");
            var result = await _deviceAppService.GetMonitorDataByCutomerForWeb(new GetMonitorDataByCutomerForWebInput() {UserId = AbpSession.GetUserId() });
            result.ShouldNotBeNull();
        }
    }
}
