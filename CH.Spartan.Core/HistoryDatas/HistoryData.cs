﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CH.Spartan.HistoryDatas
{
    public class HistoryData : Entity<long>
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 方向 0-359,正北为0,顺时针 
        /// </summary>
        public int Direction { get; set; }

        /// <summary>
        /// 海拔高度,单位为米(m) 
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 纬度 
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 经度 
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 速度 km/h 
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 接收时间 
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 上报时间 
        /// </summary>
        public DateTime ReportTime { get; set; }

    }
}
