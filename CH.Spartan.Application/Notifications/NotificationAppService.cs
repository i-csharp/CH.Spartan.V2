using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Notifications;
using CH.Spartan.Messages.Dto;

namespace CH.Spartan.Notifications
{
    public class NotificationAppService : SpartanAppServiceBase, INotificationAppService
    {

        private readonly UserNotificationManager _userNotificationManager;

        public NotificationAppService(UserNotificationManager userNotificationManager)
        {
            _userNotificationManager = userNotificationManager;
        }

        public async Task<PagedResultOutput<UserNotification>> GetNotificationListPagedAsync(GetNotificationListPagedInput input)
        {
            var result =
                await
                    _userNotificationManager.GetUserNotificationsAsync(input.UserId, input.State, input.SkipCount,
                        input.MaxResultCount, input.DeviceId?.ToString());

            return new PagedResultOutput<UserNotification>(0, result);
        }
    }
}
