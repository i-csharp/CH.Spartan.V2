using Abp.Application.Navigation;
using Abp.Localization;

namespace CH.Spartan.Infrastructure
{
    public class SpartanNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Customer",
                        L("我的主页"),//客户主页
                        url: "#",
                        icon: "fa fa-home",
                        requiresAuthentication: true,
                        requiredPermissionName: SpartanPermissionNames.CustomerManages
                        )
                        .AddItem(new MenuItemDefinition("Monitor", L("定位监控"), "fa fa-map-signs", "/CustomerManage/Monitor", true, SpartanPermissionNames.CustomerManages_Monitor))
                        .AddItem(new MenuItemDefinition("HistoryData", L("历史轨迹"), "fa fa-reply", "/CustomerManage/HistoryData", true, SpartanPermissionNames.CustomerManages_HistoryData))
                        .AddItem(new MenuItemDefinition("Notification", L("报警信息"), "fa fa-bell", "/CustomerManage/Notification", true, SpartanPermissionNames.CustomerManages_Notification))
                ).AddItem(
                    new MenuItemDefinition(
                        "CustomersManage",
                        L("我的设置"),
                        url: "#",
                        icon: "fa fa-tasks",
                        requiresAuthentication: true,
                        requiredPermissionName: SpartanPermissionNames.CustomerManages
                        )
                        .AddItem(new MenuItemDefinition("UserInfo", L("个人资料"), "fa fa-briefcase", "/CustomerManage/UserInfo", true, SpartanPermissionNames.CustomerManages_UserInfo))
                        .AddItem(new MenuItemDefinition("ChangePassword", L("修改密码"), "fa fa-key", "/CustomerManage/ChangePassword", true, SpartanPermissionNames.CustomerManages_ChangePassword))
                        .AddItem(new MenuItemDefinition("Device", L("车辆管理"), "fa fa-truck", "/CustomerManage/Device", true, SpartanPermissionNames.CustomerManages_Device))
                        .AddItem(new MenuItemDefinition("Area", L("区域管理"), "fa fa-flag-o", "/CustomerManage/Area", true, SpartanPermissionNames.CustomerManages_Area))
                        .AddItem(new MenuItemDefinition("Setting", L("我的设置"), "fa fa-tasks", "/CustomerManage/Setting", true, SpartanPermissionNames.CustomerManages_Setting))
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Agent",
                        L("客户主页"),//代理客户的主页
                        url: "#",
                        icon: "fa fa-circle-o-notch",
                        requiresAuthentication: true,
                        requiredPermissionName: SpartanPermissionNames.AgentManages
                        )
                        .AddItem(new MenuItemDefinition("Monitor", L("定位监控"), "fa fa-map-signs", "/AgentManage/Monitor", true, SpartanPermissionNames.AgentManages_Monitor))
                        .AddItem(new MenuItemDefinition("HistoryData", L("历史轨迹"), "fa fa-reply", "/AgentManage/HistoryData", true, SpartanPermissionNames.AgentManages_HistoryData))
                )
                .AddItem(
                    new MenuItemDefinition(
                        "AgentManage",
                        L("代理管理"),
                        url: "#",
                        icon: "fa fa-desktop",
                        requiresAuthentication: true,
                        requiredPermissionName: SpartanPermissionNames.AgentManages
                        )
                        .AddItem(new MenuItemDefinition("User", L("客户管理"), "fa fa-user", "/AgentManage/User", true, SpartanPermissionNames.AgentManages_User))
                        .AddItem(new MenuItemDefinition("Device", L("车辆管理"), "fa fa-truck", "/AgentManage/Device", true, SpartanPermissionNames.AgentManages_Device))
                        .AddItem(new MenuItemDefinition("DeviceStock", L("库存管理"), "fa fa-hourglass-half", "/AgentManage/DeviceStock", true, SpartanPermissionNames.AgentManages_DeviceStock))
                        .AddItem(new MenuItemDefinition("DealRecord", L("交易记录"), "fa fa-credit-card", "/AgentManage/DealRecord", true, SpartanPermissionNames.AgentManages_DealRecord))
                        .AddItem(new MenuItemDefinition("Setting", L("代理设置"), "fa fa-tasks", "/AgentManage/Setting", true, SpartanPermissionNames.AgentManages_Setting))
                )
                .AddItem(
                    new MenuItemDefinition(
                        "PlatformManage",
                        L("平台管理"),
                        url: "#",
                        icon: "fa fa-desktop",
                        requiresAuthentication: true,
                        requiredPermissionName: SpartanPermissionNames.PlatformManages
                        )
                        .AddItem(new MenuItemDefinition("DealRecord", L("交易记录"), "fa fa-credit-card", "/PlatformManage/DealRecord", true, SpartanPermissionNames.PlatformManages_DealRecord))
                ).AddItem(
                    new MenuItemDefinition(
                        "SystemManage",
                        L("系统管理"),
                        url: "#",
                        icon: "fa fa-cog",
                        requiresAuthentication: true,
                        requiredPermissionName: SpartanPermissionNames.SystemManages
                        )
                        .AddItem(new MenuItemDefinition("UserInfo", L("个人资料"), "fa fa-briefcase", "/SystemManage/UserInfo", true, SpartanPermissionNames.SystemManages_UserInfo))
                        .AddItem(new MenuItemDefinition("ChangePassword", L("修改密码"), "fa fa-key", "/SystemManage/ChangePassword", true, SpartanPermissionNames.SystemManages_ChangePassword))
                        .AddItem(new MenuItemDefinition("Tenant", L("租户管理"), "fa fa-user-secret", "/SystemManage/Tenant", true, SpartanPermissionNames.SystemManages_Tenant))
                        .AddItem(new MenuItemDefinition("DeviceType", L("设备类型"), "fa fa-square-o", "/SystemManage/DeviceType", true, SpartanPermissionNames.SystemManages_Tenant))
                        .AddItem(new MenuItemDefinition("Node", L("数据节点"), "fa fa-database", "/SystemManage/Node", true, SpartanPermissionNames.SystemManages_Node))
                        .AddItem(new MenuItemDefinition("Setting", L("系统设置"), "fa fa-tasks", "/SystemManage/Setting", true, SpartanPermissionNames.SystemManages_Setting))
                        .AddItem(new MenuItemDefinition("AuditLog", L("审计日志"), "fa fa-calendar-o", "/SystemManage/AuditLog", true, SpartanPermissionNames.SystemManages_AuditLog))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SpartanConsts.LocalizationSourceName);
        }
    }
}
