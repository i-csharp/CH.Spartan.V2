using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace CH.Spartan.Settings.Dto
{
    public class UserSettingDto : IValidate
    {
        /// <summary>
        /// 报警是否开启
        /// </summary>
        public string User_IsEnableAlarm { get; set; }

        /// <summary>
        /// 报警信息报警声音
        /// </summary>
        public string User_AlarmSound { get; set; }

        /// <summary>
        /// 报警信息是否发送邮件
        /// </summary>
        public string User_IsSendEmail { get; set; }

        /// <summary>
        /// 报警信息是否发送APP
        /// </summary>
        public string User_IsSendApp { get; set; }

        /// <summary>
        /// 接收报警类型
        /// </summary>
        public string User_ReceiveAlarmType { get; set; }

        /// <summary>
        /// 报警信息接收邮件列表
        /// </summary>
        public string User_ReceiveEmails { get; set; }

        /// <summary>
        /// 报警信息允许接收开始时间
        /// </summary>
        public string User_ReceiveStartTime { get; set; }

        /// <summary>
        /// 报警信息允许接收结束时间
        /// </summary>
        public string User_ReceiveEndTime { get; set; }

        /// <summary>
        /// 报警设防半径
        /// </summary>
        public string User_FortifyRadius { get; set; }
    }

    public class UpdateUserSettingInput : IInputDto
    {
        public UserSettingDto UserSetting { get; set; }
    }


    public class GetUpdateUserSettingOutput : IInputDto
    {
        public UserSettingDto UserSetting { get; set; }
    }
    
}
