using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Spartan.Notifications;
using CH.Spartan.Notifications.Dto;
using Shouldly;
using Xunit;

namespace CH.Spartan.Tests.Notifications
{
   public class NotificationAppService_Test: SpartanTestBase
   {
       private readonly INotificationAppService _notificationAppService;

       public NotificationAppService_Test()
       {
           _notificationAppService = Resolve<INotificationAppService>();
       }

        [Fact]
        public async Task GetUserNotificationsAsync()
        {
            var result = await _notificationAppService.GetNotificationListPagedAsync(new GetNotificationListPagedInput() { UserId = 3,MaxResultCount = 10,SkipCount = 0});
            result.ShouldNotBeNull();
        }

    }
}
