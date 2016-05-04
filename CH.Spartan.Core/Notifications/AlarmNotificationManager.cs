using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Notifications;
using Abp.Runtime.Caching;
using Abp.UI;
using CH.Spartan.Devices;
using CH.Spartan.Domain;
using CH.Spartan.Infrastructure;
using CH.Spartan.Jobs;
using CH.Spartan.Maps;
using CH.Spartan.Users;

namespace CH.Spartan.Notifications
{
    public class AlarmNotificationManager : ISingletonDependency
    {
        private readonly ISettingManager _settingManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly MapManager _mapManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly UserManager _userManager;

        public AlarmNotificationManager(
            ISettingManager settingManager,
            IUnitOfWorkManager unitOfWorkManager,
            MapManager mapManager,
            IBackgroundJobManager backgroundJobManager,
            INotificationPublisher notificationPublisher,
            UserManager userManager)
        {
            _settingManager = settingManager;
            _unitOfWorkManager = unitOfWorkManager;
            _mapManager = mapManager;
            _backgroundJobManager = backgroundJobManager;
            _notificationPublisher = notificationPublisher;
            _userManager = userManager;
        }

        [UnitOfWork]
        public virtual async Task SendAsync(AlarmNotificationData message)
        {

            User targetUser;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                targetUser = await _userManager.FindByIdAsync(message.UserId);
                if (targetUser == null)
                {
                    throw new UserFriendlyException("不存在用户: " + message.UserId);
                }
            }

            //推送Web
            if (message.Latitude.HasValue && message.Longitude.HasValue)
            {
                message.Address =
                    _mapManager.GetLocation(new MapPoint(message.Latitude.Value, message.Longitude.Value))
                        .ToString();
            }

            await
                _notificationPublisher.PublishAsync(
                    SpartanConsts.YouHaveAAlarmMessage,
                    message,
                    new EntityIdentifier(typeof (Device), message.DeviceId),
                    message.Severity,
                    new[] {targetUser.Id}
                    );

            //发送邮件
            var isSendEmail =
                await
                    _settingManager.GetSettingValueForUserAsync<bool>(SpartanSettingKeys.User_IsSendEmail,
                        targetUser.TenantId, targetUser.Id);

            if (isSendEmail)
            {
                await _backgroundJobManager.EnqueueAsync<SendEmailJob, SendEmailJobArgs>(
                    new SendEmailJobArgs
                    {
                        Subject = message.Title,
                        Body = $"{message.Content} {message.Address}",
                        TargetTenantId = targetUser.TenantId,
                        TargetUserId = targetUser.Id
                    });
            }

            //推送APP
            var isSendApp =
                await
                    _settingManager.GetSettingValueForUserAsync<bool>(SpartanSettingKeys.User_IsSendApp,
                        targetUser.TenantId, targetUser.Id);

            if (isSendApp)
            {
                await _backgroundJobManager.EnqueueAsync<SendAppJob, SendAppJobArgs>(
                    new SendAppJobArgs
                    {
                        Subject = message.Title,
                        Body = $"{message.Content} {message.Address}",
                        TargetTenantId = targetUser.TenantId,
                        TargetUserId = targetUser.Id
                    });
            }

        }
    }
}
