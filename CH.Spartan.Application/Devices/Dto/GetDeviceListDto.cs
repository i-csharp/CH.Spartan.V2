﻿using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Devices.Dto
{
    [AutoMapFrom(typeof(Device))]
    public class GetDeviceListDto : AuditedEntityDto
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
        public string BDescription { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string BNo { get; set; }

        /// <summary>
        /// Sim号
        /// </summary>
        public string BSimNo { get; set; }

        /// <summary>
        /// 所属租户
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// 所属客户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public string IsOnlineText { get; set; }
        /// <summary>
        /// 通讯时间
        /// </summary>
        public DateTime? GReceiveTime { get; set; }

        /// <summary>
        /// 是否定位
        /// </summary>
        public string IsLocatedText { get; set; }
        /// <summary>
        /// 定位时间
        /// </summary>
        public DateTime? GReportTime { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public string IsExpireText { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? BExpireTime { get; set; }

        
    }


    [AutoMapFrom(typeof(Device))]
    public class GetNearExpireDeviceListDto : AuditedEntityDto
    {
        /// <summary>
        /// 设备名字
        /// </summary>
        public string BName { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? BExpireTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string ExpireText { get; set; }

        /// <summary>
        /// 过期百分比
        /// </summary>
        public double ExpirePercent { get; set; }

        /// <summary>
        /// 过期百分比 样式
        /// </summary>
        public string ExpirePercentClsText { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 所属客户
        /// </summary>
        public string UserName { get; set; }

    }

    [AutoMapFrom(typeof(Device))]
    public class GetLastDeviceListDto : AuditedEntityDto
    {
        /// <summary>
        /// 设备名字
        /// </summary>
        public string BName { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string BNo { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? BExpireTime { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 所属客户
        /// </summary>
        public string UserName { get; set; }

    }

    public class GetDeviceListInput : QueryListResultRequestInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }
    }

    public class GetDeviceListPagedInput : QueryListPagedResultRequestInput
    {

        /// <summary>
        /// 设备类型Id
        /// </summary>
        public int? DeviceTypeId { get; set; }
        
        /// <summary>
        /// 是否定位
        /// </summary>
        public bool? IsLocated { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }
    }

    public class GetNearExpireDeviceListInput : QueryListResultRequestInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }
    }

    public class GetLastDeviceListInput : QueryListResultRequestInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }
    }
}
