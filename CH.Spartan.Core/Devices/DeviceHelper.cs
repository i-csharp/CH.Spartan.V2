
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Json;
using Abp.Localization;
using CH.Spartan.Areas;
using CH.Spartan.Infrastructure;
using Abp.Extensions;
namespace CH.Spartan.Devices
{
    public static class DeviceHelper
    {
        #region Common
        /// <summary>
        /// 5分钟内有数据上次上来 都属于在线状态
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool IsOnline(Device device)
        {
            return device.GReceiveTime > DateTime.Now.AddMinutes(SpartanConsts.DefaultOfflineMinutes);
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool IsExpire(Device device)
        {
            return device.BExpireTime < DateTime.Now;
        }

        /// <summary>
        /// 获取当前区域设置
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static List<AreaSetting> GetOutAreaSettings(Device device)
        {
            if (!device.SInOutArea.IsNullOrEmpty())
            {
                return device.SInOutArea.ToObject<List<AreaSetting>>();
            }
            return new List<AreaSetting>();
        }

        /// <summary>
        /// 获取方向
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetDirectionText(Device device)
        {
            if (device.GDirection > 27 && device.GDirection <= 72)
            {
                return L("东北");
            }
            if (device.GDirection > 72 && device.GDirection <= 117)
            {
                return L("正东");
            }
            if (device.GDirection > 117 && device.GDirection <= 162)
            {
                return L("东南");
            }
            if (device.GDirection > 162 && device.GDirection <= 207)
            {
                return L("正南");
            }
            if (device.GDirection > 207 && device.GDirection <= 252)
            {
                return L("西南");
            }
            if (device.GDirection > 252 && device.GDirection <= 297)
            {
                return L("正西");
            }
            if (device.GDirection > 297 && device.GDirection <= 342)
            {
                return L("西北");
            }
            return L("正北");
        }

        /// <summary>
        /// 获取图标地址
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetIconUrl(Device device)
        {
            var type = device.BIconType;
            var status = IsOnline(device) ? "green" : "gray";
            var icon = "6.gif";
            if (device.GDirection > 27 && device.GDirection <= 72)
            {
                icon = "5.gif";
            }
            else if (device.GDirection > 72 && device.GDirection <= 117)
            {
                icon = "4.gif";
            }
            else if (device.GDirection > 117 && device.GDirection <= 162)
            {
                icon = "3.gif";
            }
            else if (device.GDirection > 162 && device.GDirection <= 207)
            {
                icon = "2.gif";
            }
            else if (device.GDirection > 207 && device.GDirection <= 252)
            {
                icon = "1.gif";
            }
            else if (device.GDirection > 252 && device.GDirection <= 297)
            {
                icon = "8.gif";
            }
            else if (device.GDirection > 297 && device.GDirection <= 342)
            {
                icon = "7.gif";
            }
            return $"/Content/img/cars/{type}/{status}/{icon}";
        }



        /// <summary>
        /// 获取速度
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetSpeedText(Device device)
        {
            string text;
            if (!IsOnline(device) || device.GSpeed < 2)
            {
                text = L("静止");
                if (device.CLastHaveSpeedTime.HasValue)
                {
                    text += " (" + DateTime.Now.GetDateDiff(device.CLastHaveSpeedTime.Value) + ")";
                }
                return text;
            }
            text = device.GSpeed + " km/h";
            return text;
        }


        /// <summary>
        /// 获取Gps状态
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetGpsStatusText(Device device)
        {
            var result = new StringBuilder();
            result.Append((IsOnline(device) ? L("在线") : L("离线")) + " ");
            result.Append(GetDirectionText(device) + " ");
            result.Append((device.GIsLocated ? L("已定位") : L("未定位")) + " ");
            return result.ToString();
        }

        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetDeviceStatusText(Device device)
        {
            var result = new StringBuilder();

            if (device.DeviceType.IsHaveFortify)
            {
                result.Append((device.CIsFortify ? L("已设防") : L("未设防")) + " ");
            }

            if (device.DeviceType.IsHaveAcc)
            {
                result.Append((device.CIsAccOn ? L("点火") : L("熄火")) + " ");
            }

            if (device.DeviceType.IsHavePower)
            {
                result.Append($"电量[{device.CPower}%]" + " ");
            }

            if (device.DeviceType.IsHaveRelay1)
            {
                result.Append((device.CIsRelay1Enable ? L("R1开启") : L("R1关闭")) + " ");
            }
            if (device.DeviceType.IsHaveRelay2)
            {
                result.Append((device.CIsRelay2Enable ? L("R2开启") : L("R2关闭")) + " ");
            }

            if (device.DeviceType.IsHaveRelay3)
            {
                result.Append((device.CIsRelay3Enable ? L("R3开启") : L("R3关闭")) + " ");
            }

            return result.ToString();
        }

        /// <summary>
        /// 获取报警信息
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetAlarmText(Device device)
        {
            var result = new StringBuilder();
            if (device.DeviceType.IsHaveDropOff)
            {
                result.Append(device.CIsDropOff ? L("脱落") + " " : "");
            }
            if (device.DeviceType.IsHaveSos)
            {
                result.Append(device.CIsSos ? L("紧急") + " " : "");
            }

            if (device.DeviceType.IsHaveLowBattery)
            {
                result.Append(device.CIsLowBattery ? L("低电") + " " : "");
            }

            if (device.DeviceType.IsHaveShake)
            {
                result.Append(device.CIsShake ? L("震动") + " " : "");
            }

            if (device.DeviceType.IsHaveMainPowerBreak)
            {
                result.Append(device.CIsMainPowerBreak ? L("断电") + " " : "");
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取离线时间
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetOfflineText(Device device)
        {
            if (device.GReceiveTime.HasValue && DateTime.Now > device.GReceiveTime && !IsOnline(device))
            {
                return DateTime.Now.GetDateDiff(device.GReceiveTime.Value);
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取离线时间
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetClsText(Device device)
        {
            var cls = "success-element";
            if (!IsOnline(device))
            {
                cls= "danger-element";
            }

            if (IsExpire(device))
            {
                cls= "warning-element";
            }

            return cls;
        }
        /// <summary>
        /// 获取通讯时间
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetReceiveTimeText(Device device)
        {
            return device.GReceiveTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-";
        }

        /// <summary>
        /// 获取定位时间
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetReportTimeText(Device device)
        {
            return device.GReportTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-";
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetExpireText(Device device)
        {
            if (device.BExpireTime.HasValue)
            {
                if (IsExpire(device))
                {
                    return L("已过期") + " (" + DateTime.Now.GetDateDiff(device.BExpireTime.Value) + ")";
                }
                return L("还有") + " (" + DateTime.Now.GetDateDiff(device.BExpireTime.Value) + ")过期";
            }

            return L("终身免费");
        }

        /// <summary>
        /// 本地语言
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string L(string name)
        {
            return LocalizationHelper.GetString(SpartanConsts.LocalizationSourceName, name);
        } 
        #endregion
       
        #region Web
        public static string WinGpsStatusText(Device device)
        {
            return GetGpsStatusText(device);
        }
        public static string WinDeviceStatusText(Device device)
        {
            return GetDeviceStatusText(device);
        }

        public static string WinAlarmText(Device device)
        {
            return GetAlarmText(device);
        }

        public static string WinReceiveTimeText(Device device)
        {
            return GetReceiveTimeText(device);
        }

        public static string WinReportTimeText(Device device)
        {
            return GetReportTimeText(device);
        }

        public static string WinSpeedText(Device device)
        {
            return GetSpeedText(device);
        }

        public static string WinOfflineText(Device device)
        {
            return GetOfflineText(device);
        }

        public static string PanelGpsStatusText(Device device)
        {
            return GetGpsStatusText(device);
        }

        public static string PanelSpeedText(Device device)
        {
            return GetSpeedText(device);
        }

        public static string ExpireText(Device device)
        {
            return GetExpireText(device);
        }

        public static string IconUrl(Device device)
        {
            return GetIconUrl(device);
        }

        public static string ClsText(Device device)
        {
            return GetClsText(device);
        }
        #endregion

        #region App

        #endregion


    }
}
