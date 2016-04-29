using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using CH.Spartan.Devices.Dto;
using CH.Spartan.HistoryDatas;
using CH.Spartan.HistoryDatas.Dto;
using CH.Spartan.Infrastructure;
using Shouldly;
using Xunit;

namespace CH.Spartan.Tests.HistoryDatas
{
    public class HistoryDataService_Test : SpartanTestBase
    {
        private readonly IHistoryDataAppService _historyDataAppService;

        public HistoryDataService_Test()
        {
            _historyDataAppService = Resolve<IHistoryDataAppService>();
        }

        [Fact]
        public async Task GetMonitorDataByCutomerForWeb()
        {
            LoginAsTenant("yugps", "yugps");
            var result =await _historyDataAppService.GetHistoryDataForWebAsync(new GetHistoryDataForWebInput()
            {
                DeviceId = 2171,
                ParkingTime = 5,
                Coordinates = EnumCoordinates.Gcj02,
                StartTime = DateTime.Now.AddMonths(-1),
                EndTime = DateTime.Now
            });
            result.ShouldNotBeNull();
        }
    }
}
