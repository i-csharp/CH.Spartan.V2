using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace CH.Spartan.Settings.Dto
{
    public class GeneralSettingDto : IValidate
    {
        /// <summary>
        /// Host 首页
        /// </summary>
        public string General_Host_Index { get; set; }

        /// <summary>
        /// Tenant 首页
        /// </summary>
        public string General_Tenant_Index { get; set; }

        /// <summary>
        /// User 首页
        /// </summary>
        public string General_User_Index { get; set; }

        /// <summary>
        /// 是否开启发送邮件
        /// </summary>
        public bool General_Mail_IsEnable { get; set; }

        /// <summary>
        /// 发送人邮箱密码
        /// </summary>
        public string General_Mail_Password { get; set; }

        /// <summary>
        /// 发送服务器端口号
        /// </summary>
        public string General_Mail_Port { get; set; }

        /// <summary>
        /// 发送人邮箱
        /// </summary>
        public string General_Mail_Send_Email { get; set; }

        /// <summary>
        /// 发送邮件服务器(Smtp)
        /// </summary>
        public string General_Mail_Smtp { get; set; }

        /// <summary>
        /// 发送人邮箱名字
        /// </summary>
        public string General_Mail_UserName { get; set; }

        /// <summary>
        /// 高德地图密钥(ak)
        /// </summary>
        public string General_Map_AMapAk { get; set; }

        /// <summary>
        /// 高德地图密钥(ak) 地理解析
        /// </summary>
        public string General_Map_AMapAk_Api { get; set; }

        /// <summary>
        /// 百度地图密钥(ak)
        /// </summary>
        public string General_Map_BaiduAk { get; set; }

        /// <summary>
        /// 推送AppKey
        /// </summary>
        public string General_Push_AppKey { get; set; }

        /// <summary>
        /// 允许推送
        /// </summary>
        public bool General_Push_IsEnable { get; set; }

        /// <summary>
        /// 推送MasterSecret
        /// </summary>
        public string General_Push_Master_Secret { get; set; }


        /// <summary>
        /// QQ登录接入Id
        /// </summary>
        public string General_QQ_AppId { get; set; }

        /// <summary>
        /// QQ登录接入密钥
        /// </summary>
        public string General_QQ_AppSecrett { get; set; }


        /// <summary>
        /// 微信接口地址
        /// </summary>
        public string General_WeChat_ApiUrl { get; set; }

        /// <summary>
        /// 微信应用接入Id
        /// </summary>
        public string General_WeChat_AppId { get; set; }

        /// <summary>
        /// 微信应用接入密钥
        /// </summary>
        public string General_WeChat_AppSecret { get; set; }

        /// <summary>
        /// 微信应用账号
        /// </summary>
        public string General_WeChat_Id { get; set; }

        /// <summary>
        /// 微信应用加密令牌
        /// </summary>
        public string General_WeChat_Token { get; set; }

        /// <summary>
        /// 微博登录接入Id
        /// </summary>
        public string General_WeiBo_AppId { get; set; }

        /// <summary>
        /// 微博登录接入密钥
        /// </summary>
        public string General_WeiBo_AppSecret { get; set; }

        /// <summary>
        /// 微信登录接入Id
        /// </summary>
        public string General_WeiXin_AppId { get; set; }

        /// <summary>
        /// 微信登录接入密钥
        /// </summary>
        public string General_WeiXin_AppSecret { get; set; }

        /// <summary>
        /// 网关事件队列名字
        /// </summary>
        public string General_ActiveMq_Gateway_Event_Name { get; set; }

        /// <summary>
        /// 网关事件队列地址
        /// </summary>
        public string General_ActiveMq_Gateway_Event_Uri { get; set; }

        /// <summary>
        /// Web事件队列名字
        /// </summary>
        public string General_ActiveMq_Web_Event_Name { get; set; }

        /// <summary>
        /// Web事件队列地址
        /// </summary>
        public string General_ActiveMq_Web_Event_Uri { get; set; }
    }

    public class UpdateGeneralSettingInput : IInputDto
    {
        public GeneralSettingDto GeneralSetting { get; set; }
    }

    public class GetUpdateGeneralSettingOutput : IInputDto
    {
        public GeneralSettingDto GeneralSetting { get; set; }
    }
}
