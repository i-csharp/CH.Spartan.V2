using System;

namespace CH.Spartan.Infrastructure
{
    #region 常量
    public class SpartanConsts
    {
        public const string LocalizationSourceName = "Spartan";
        public const int DefaultMaxResultCount = 10;
        public const string DefaultSorting = "Id DESC";
        public const string DefaultPassword = "123456";
        public const string DefaultAnimate = "animated fadeInRight";
        public const int DefaultNodeMaxUsageCount = 1000;
        public const int DefaultOfflineMinutes = -5;
        public const int DefaultLimitSpeed = 80;
        public const string YouHaveAAlarmMessage = "YouHaveAAlarmMessage";
    }
    #endregion

    #region 枚举

    public enum EnumCoordinates
    {
        //地球坐标系
        Wgs84 = 0,
        //火星坐标系
        Gcj02 = 1
    }

    public enum EnumDealRecordType
    {
        [EnumDisplayName("充值")]
        Deposit = 1,
        [EnumDisplayName("安装设备")]
        InstallDevice = 2
    }

    public enum EnumCodeCreateRule
    {
        [EnumDisplayName("设备号")]
        No=0,
        [EnumDisplayName("前缀0加设备号")]
        PrefixZeroAndNo =1
    }

    [Flags]
    public enum EnumAlarmType
    {
        [EnumDisplayName("离线报警")]
        OffLine =1,
        [EnumDisplayName("断电报警")]
        MainPowerBreak = 2,
        [EnumDisplayName("SOS求救")]
        Sos = 4,
        [EnumDisplayName("超速报警")]
        OverSpeed = 8,
        [EnumDisplayName("进入区域")]
        InArea = 16,
        [EnumDisplayName("离开设防")]
        OutFortify = 32,
        [EnumDisplayName("脱落报警")]
        DropOff = 64,
        [EnumDisplayName("震动报警")]
        Shake = 128,
        [EnumDisplayName("低电报警")]
        LowBattery = 256,
        [EnumDisplayName("进入盲区")]
        InBlindArea = 512,
        [EnumDisplayName("离开盲区")]
        OutBlindArea = 1024,
        [EnumDisplayName("启动报警")]
        Startup = 2048
    }

    public enum EnumGetwayEventDataType
    {
        [EnumDisplayName("报警通知")]
        AlarmNotificationData = 0
    }

    public enum EnumWebEventDataType
    {
        [EnumDisplayName("设备指令")]
        InstructionData = 0,
        [EnumDisplayName("设备已更新")]
        DeviceUpdated=1,
        [EnumDisplayName("设备已删除")]
        DeviceDeleted = 1,
        [EnumDisplayName("用户配置已更新")]
        UserSettingUpdated = 2
    }

    public enum EnumInstructionType
    {
        [EnumDisplayName("继电器1")]
        Relay1 = 1,
        [EnumDisplayName("继电器2")]
        Relay2 = 2,
        [EnumDisplayName("继电器3")]
        Relay3 =3,
    }

    #endregion
}