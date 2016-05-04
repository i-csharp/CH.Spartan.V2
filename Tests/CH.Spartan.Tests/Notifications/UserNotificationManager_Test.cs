using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Notifications;
using CH.Spartan.Maps;
using CH.Spartan.Messages;
using CH.Spartan.Messages.Dto;
using Shouldly;
using Xunit;

namespace CH.Spartan.Tests.Notifications
{
   public class UserNotificationManager_Test: SpartanTestBase
    {
        private readonly UserNotificationManager _userNotificationManager;
        public UserNotificationManager_Test()
        {
            _userNotificationManager = Resolve<UserNotificationManager>();
        }

        [Fact]
        public async Task GetUserNotificationsAsync()
        {
            var result = await _userNotificationManager.GetUserNotificationsAsync(3);
            result.ShouldNotBeNull();
        }
    }
}
