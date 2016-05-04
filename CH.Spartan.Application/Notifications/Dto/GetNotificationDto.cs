using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Notifications;

namespace CH.Spartan.Notifications.Dto
{
    public class GetNotificationDto:IDto
    {

    }

    public class GetNotificationCountInput : IInputDto
    {
        [Range(1,long.MaxValue)]
        public long UserId { get; set; }
        public UserNotificationState? State { get; set; }
    }
}
