using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using Abp.Localization;
using AutoMapper;
using CH.Spartan.Areas.Dto;
using CH.Spartan.DealRecords;
using CH.Spartan.DealRecords.Dto;
using CH.Spartan.Devices;
using CH.Spartan.Devices.Dto;
using CH.Spartan.Infrastructure;
using CH.Spartan.MultiTenancy;
using CH.Spartan.MultiTenancy.Dto;
using CH.Spartan.Users;
using CH.Spartan.Users.Dto;

namespace CH.Spartan
{
   public static class SpartanMapping
    {
       public static void Map()
       {
           MapCommon();
           MapDevice();
       }

       private static void MapCommon()
       {
           Mapper.CreateMap<Tenant, GetTenantListDto>()
               .ForMember(d => d.IsActiveText, opt => opt.MapFrom(o => o.IsActive ? L("是") : L("否")));

           Mapper.CreateMap<User, GetUserListDto>()
               .ForMember(d => d.IsActiveText, opt => opt.MapFrom(o => o.IsActive ? L("是") : L("否")))
               .ForMember(d => d.TenantText, opt => opt.MapFrom(o => o.Tenant != null ? o.Tenant.Name : "-"));

           Mapper.CreateMap<Device, UpdateDeviceByAgentDto>()
               .ForMember(d => d.AreaSettings, opt => opt.MapFrom(o => DeviceHelper.GetOutAreaSettings(o).MapTo<List<AreaSettingDto>>()));

           Mapper.CreateMap<Device, UpdateDeviceByCustomerDto>()
               .ForMember(d => d.AreaSettings, opt => opt.MapFrom(o => DeviceHelper.GetOutAreaSettings(o).MapTo<List<AreaSettingDto>>()));

           Mapper.CreateMap<DealRecord, GetDealRecordListDto>()
               .ForMember(d => d.IsSucceedText, opt => opt.MapFrom(o => o.IsSucceed ? L("是") : L("否")))
               .ForMember(d => d.TenantText, opt => opt.MapFrom(o => o.Tenant != null ? o.Tenant.Name : "-"))
               .ForMember(d => d.TypeText, opt => opt.MapFrom(o => L(o.Type.GetDisplayName())));
       }

       private static void MapDevice()
       {
            //列表
            Mapper.CreateMap<Device, GetDeviceListDto>()
                   .ForMember(d => d.TenancyName, opt => opt.MapFrom(o => o.Tenant != null ? o.Tenant.Name : ""))
                   .ForMember(d => d.UserName, opt => opt.MapFrom(o => o.User != null ? o.User.Name : ""))
                   .ForMember(d => d.DeviceTypeName, opt => opt.MapFrom(o => o.DeviceType != null ? o.DeviceType.Name : ""))
                   .ForMember(d => d.IsOnlineText, opt => opt.MapFrom(o => DeviceHelper.IsOnline(o) ? L("是") : L("否")))
                   .ForMember(d => d.IsExpireText, opt => opt.MapFrom(o => DeviceHelper.IsExpire(o) ? L("是") : L("否")))
                   .ForMember(d => d.IsLocatedText, opt => opt.MapFrom(o => o.GIsLocated ? L("是") : L("否")));

           //监控数据
           Mapper.CreateMap<Device, GetMonitorDataByCutomerForWebDto>()
               .ForMember(p => p.IsOnline, opt => opt.MapFrom(o => DeviceHelper.IsOnline(o)))
               .ForMember(p => p.IsExpire, opt => opt.MapFrom(o => DeviceHelper.IsExpire(o)))
               .ForMember(p => p.IsStatic, opt => opt.MapFrom(o => DeviceHelper.IsStatic(o)))
               .ForMember(p => p.IconUrl, opt => opt.MapFrom(o => DeviceHelper.WebWebIconUrl(o)))
               .ForMember(p => p.WinGpsStatusText, opt => opt.MapFrom(o => DeviceHelper.WebWinGpsStatusText(o)))
               .ForMember(p => p.WinDeviceStatusText, opt => opt.MapFrom(o => DeviceHelper.WebWinDeviceStatusText(o)))
               .ForMember(p => p.WinAlarmStatusText, opt => opt.MapFrom(o => DeviceHelper.WebWinAlarmStatusText(o)))
               .ForMember(p => p.WinReceiveTimeText, opt => opt.MapFrom(o => DeviceHelper.WebWinReceiveTimeText(o)))
               .ForMember(p => p.WinReportTimeText, opt => opt.MapFrom(o => DeviceHelper.WebWinReportTimeText(o)))
               .ForMember(p => p.WinSpeedText, opt => opt.MapFrom(o => DeviceHelper.WebWinSpeedText(o)))
               .ForMember(p => p.WinOfflineText, opt => opt.MapFrom(o => DeviceHelper.WebWinOfflineText(o)))
               .ForMember(p => p.PanelGpsStatusText, opt => opt.MapFrom(o => DeviceHelper.WebPanelGpsStatusText(o)))
               .ForMember(p => p.PanelSpeedText, opt => opt.MapFrom(o => DeviceHelper.WebPanelSpeedText(o)))
               .ForMember(p => p.PanelExpireText, opt => opt.MapFrom(o => DeviceHelper.WebPanelExpireText(o)))
               .ForMember(p => p.PanelClsText, opt => opt.MapFrom(o => DeviceHelper.WebPanelClsText(o)));
        }

       private static string L(string name)
       {
           return LocalizationHelper.GetString(SpartanConsts.LocalizationSourceName, name);
       }
    }
}
