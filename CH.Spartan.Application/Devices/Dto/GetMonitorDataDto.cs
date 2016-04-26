using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Devices.Dto
{
    public class GetMonitorDataDto : EntityDto
    {
        /// <summary>
        /// 设备名字
        /// </summary>
        public string BName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeName { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string BDscription { get; set; }

    }

    public class GetMonitorDataInput : QueryListResultRequestInput
    {
        /// <summary>
        /// 所属用户
        /// </summary>
        [Range(1, long.MaxValue)]
        public long? UserId { get; set; }
    }

    public class GetMonitorDataOutput<T> : ListResultOutput<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 总数 在线
        /// </summary>
        public int TotalOnline { get; set; }

        /// <summary>
        /// 总数 离线
        /// </summary>
        public int TotalOffline => Total - TotalOnline;

        /// <summary>
        /// 总数 过期
        /// </summary>
        public int TotalExpire { get; set; }
    }
}
