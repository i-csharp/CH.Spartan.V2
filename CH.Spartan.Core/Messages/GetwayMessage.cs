using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Notifications;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Messages
{
    [Serializable]
    public class GetwayMessage : NotificationData
    {
        public long UserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// 严重级别
        /// </summary>
        public NotificationSeverity Severity { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public EnumAlarmType? AlarmType { get; set; }

        /// <summary>
        /// 报警地址
        /// </summary>
        public string Address { get; set; }
    }
}
