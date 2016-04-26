using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Devices.Dto
{
    #region Common
    public class GetMonitorDataByCutomerInput : GetMonitorDataInput
    {
       
    }

    public class GetMonitorDataByCutomerOutput<T> : GetMonitorDataOutput<T>
    {

    }

    #endregion

    #region Web

    [AutoMapFrom(typeof(Device))]
    public class GetMonitorDataByCutomerForWebDto : GetMonitorDataDto
    {
        /// <summary>
        /// 窗口Gps状态
        /// </summary>
        public string WinGpsStatusText { get; set; }

        /// <summary>
        /// 窗口设备状态
        /// </summary>
        public string WinDeviceStatusText { get; set; }
        
        /// <summary>
        /// 窗口报警信息
        /// </summary>
        public string WinAlarmText { get; set; }

        /// <summary>
        /// 窗口接收
        /// </summary>
        public string WinReceiveTimeText { get; set; }

        /// <summary>
        /// 窗口定位
        /// </summary>
        public string WinReportTimeText { get; set; }

        /// <summary>
        /// 窗口速度
        /// </summary>
        public string WinSpeedText { get; set; }

        /// <summary>
        /// 窗口离线
        /// </summary>
        public string WinOfflineText { get; set; }

        /// <summary>
        /// 面板Gps状态
        /// </summary>
        public string PanelGpsStatusText { get; set; }

        /// <summary>
        /// 面板速度
        /// </summary>
        public string PanelSpeedText { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string ExpireText { get; set; }

    }

    public class GetMonitorDataByCutomerForWebInput : GetMonitorDataByCutomerInput
    {

    }

    public class GetMonitorDataByCutomerForWebOutput : GetMonitorDataByCutomerOutput<GetMonitorDataByCutomerForWebDto>
    {

    }
    #endregion

    #region App

    [AutoMapFrom(typeof(Device))]
    public class GetMonitorDataByCutomerForAppDto : GetMonitorDataDto
    {

    }

    public class GetMonitorDataByCutomerForAppInput : GetMonitorDataByCutomerInput
    {

    }

    public class GetMonitorDataByCutomerForAppOutput : GetMonitorDataByCutomerOutput<GetMonitorDataByCutomerForAppDto>
    {
        
    }
    #endregion
}
