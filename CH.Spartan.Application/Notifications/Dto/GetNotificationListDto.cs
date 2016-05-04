using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Notifications;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Notifications.Dto
{
    [AutoMap(typeof(UserNotification))]
    public class GetNotificationListDto : EntityDto<Guid>
    {
        public string NotificationName { get; set; }
        
        public NotificationData Data { get; set; }
       
        public object EntityId { get; set; }
        
        public NotificationSeverity Severity { get; set; }

        public DateTime CreationTime { get; set; }
        
        public UserNotificationState State { get; set; }

        public string ClsText {
            get
            {
                //*-bg
                switch (Severity)
                {
                    case NotificationSeverity.Info:
                        return "blue";
                    case NotificationSeverity.Success:
                        return "navy";
                    case NotificationSeverity.Warn:
                        return "yellow";
                    case NotificationSeverity.Error:
                        return "red";
                    case NotificationSeverity.Fatal:
                        return "red";
                    default:
                        return "blue";
                }
            }
        }
    }


    public class GetNotificationListPagedInput : QueryListPagedResultRequestInput
    {
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }

        public int? DeviceId { get; set; }

        public UserNotificationState? State { get; set; }
    }
}
