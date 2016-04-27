using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using CH.Spartan.Devices.Dto;
using CH.Spartan.Maps;
using Shouldly;
using Xunit;

namespace CH.Spartan.Tests.Maps
{
   public class MapManager_Test: SpartanTestBase
   {
       private readonly MapManager _mapManager;

        public MapManager_Test()
        {
            _mapManager = Resolve<MapManager>();
        }

        [Fact]
        public async Task GetLocation()
        {
            var result = _mapManager.GetLocation(new MapPoint("39.990464,116.481488"));
            result.Info.ShouldBeUnique("OK");
        }
    }
}
