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

namespace CH.Spartan.Messages.Dto
{

    public class GetNotificationListPagedInput : QueryListPagedResultRequestInput
    {
        [Range(1,long.MaxValue)]
        public long UserId { get; set; }

        public int? DeviceId { get; set; }

        public UserNotificationState? State { get; set; }
    }
}
