using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using Abp.Notifications;
using CH.Spartan.Notifications.Dto;

namespace CH.Spartan.Notifications
{
    public interface INotificationAppService : IDomainService
    {
        /// <summary>
        /// 获取 通知 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<GetNotificationListDto>> GetNotificationListPagedAsync(GetNotificationListPagedInput input);

        /// <summary>
        /// 获取 通知 数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> GetNotificationCountAsync(GetNotificationCountInput input);
    }
}
