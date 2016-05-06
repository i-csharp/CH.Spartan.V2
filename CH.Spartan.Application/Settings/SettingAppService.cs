using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Runtime.Session;
using CH.Spartan.Infrastructure;
using CH.Spartan.Settings.Dto;

namespace CH.Spartan.Settings
{
    public class SettingAppService : SpartanAppServiceBase, ISettingAppService
    {
       
        public async Task<GetUpdateGeneralSettingOutput> GetUpdateGeneralSettingAsync()
        {
            var output = new GetUpdateGeneralSettingOutput
            {
                GeneralSetting = new GeneralSettingDto
                {
                    General_Host_Index = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Host_Index),
                    General_Tenant_Index = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Tenant_Index),
                    General_User_Index = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_User_Index),
                    General_Mail_IsEnable = SettingManager.GetSettingValueForApplication<bool>(SpartanSettingKeys.General_Mail_IsEnable),
                    General_Mail_Password = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Mail_Password),
                    General_Mail_Port = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Mail_Port),
                    General_Mail_Send_Email = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Mail_Send_Email),
                    General_Mail_Smtp = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Mail_Smtp),
                    General_Mail_UserName = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Mail_UserName),
                    General_Map_AMapAk = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Map_AMapAk),
                    General_Map_AMapAk_Api = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Map_AMapAk_Api),
                    General_Map_BaiduAk = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Map_BaiduAk),
                    General_Push_AppKey = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Push_AppKey),
                    General_Push_IsEnable = SettingManager.GetSettingValueForApplication<bool>(SpartanSettingKeys.General_Push_IsEnable),
                    General_Push_Master_Secret = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Push_Master_Secret),
                    General_QQ_AppId = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_QQ_AppId),
                    General_QQ_AppSecrett = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_QQ_AppSecrett),
                    General_WeChat_ApiUrl = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeChat_ApiUrl),
                    General_WeChat_AppId = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeChat_AppId),
                    General_WeChat_AppSecret = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeChat_AppSecret),
                    General_WeChat_Id = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeChat_Id),
                    General_WeChat_Token = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeChat_Token),
                    General_WeiBo_AppId = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeiBo_AppId),
                    General_WeiBo_AppSecret = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeiBo_AppSecret),
                    General_WeiXin_AppId = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeiXin_AppId),
                    General_WeiXin_AppSecret = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_WeiXin_AppSecret),
                    General_ActiveMq_Gateway_Event_Name = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Name),
                    General_ActiveMq_Gateway_Event_Uri = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Uri),
                    General_ActiveMq_Web_Event_Name = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Web_Event_Name),
                    General_ActiveMq_Web_Event_Uri = SettingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Web_Event_Uri)
                }
            };

            return await Task.FromResult(output) ;
        }

        public async Task UpdateGeneralSettingAsync(UpdateGeneralSettingInput input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Host_Index, input.GeneralSetting.General_Host_Index);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Tenant_Index, input.GeneralSetting.General_Tenant_Index);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_User_Index, input.GeneralSetting.General_User_Index);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Mail_IsEnable, input.GeneralSetting.General_Mail_IsEnable.ToString());
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Mail_Password, input.GeneralSetting.General_Mail_Password);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Mail_Port, input.GeneralSetting.General_Mail_Port);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Mail_Send_Email, input.GeneralSetting.General_Mail_Send_Email);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Mail_Smtp, input.GeneralSetting.General_Mail_Smtp);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Mail_UserName, input.GeneralSetting.General_Mail_UserName);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Map_AMapAk, input.GeneralSetting.General_Map_AMapAk);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Map_AMapAk_Api, input.GeneralSetting.General_Map_AMapAk_Api);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Map_BaiduAk, input.GeneralSetting.General_Map_BaiduAk);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Push_IsEnable, input.GeneralSetting.General_Push_IsEnable.ToString());
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_Push_Master_Secret, input.GeneralSetting.General_Push_Master_Secret);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_QQ_AppId, input.GeneralSetting.General_QQ_AppId);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_QQ_AppSecrett, input.GeneralSetting.General_QQ_AppSecrett);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeChat_ApiUrl, input.GeneralSetting.General_WeChat_ApiUrl);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeChat_AppId, input.GeneralSetting.General_WeChat_AppId);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeChat_AppSecret, input.GeneralSetting.General_WeChat_AppSecret);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeChat_Id, input.GeneralSetting.General_WeChat_Id);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeChat_Token, input.GeneralSetting.General_WeChat_Token);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeiBo_AppId, input.GeneralSetting.General_WeiBo_AppId);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeiBo_AppSecret, input.GeneralSetting.General_WeiBo_AppSecret);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeiXin_AppId, input.GeneralSetting.General_WeiXin_AppId);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_WeiXin_AppSecret, input.GeneralSetting.General_WeiXin_AppSecret);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Name, input.GeneralSetting.General_ActiveMq_Gateway_Event_Name);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_ActiveMq_Gateway_Event_Uri, input.GeneralSetting.General_ActiveMq_Gateway_Event_Uri);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_ActiveMq_Web_Event_Name, input.GeneralSetting.General_ActiveMq_Web_Event_Name);
            await SettingManager.ChangeSettingForApplicationAsync(SpartanSettingKeys.General_ActiveMq_Web_Event_Uri, input.GeneralSetting.General_ActiveMq_Web_Event_Uri);


        }

        public async Task<GetUpdateTenantSettingOutput> GetUpdateTenantSettingAsync()
        {
            var output = new GetUpdateTenantSettingOutput
            {
                TenantSetting = new TenantSettingDto
                {
                    Tenant_Customer_InstallDevice_ExpireYear = SettingManager.GetSettingValueForTenant(SpartanSettingKeys.Tenant_Customer_InstallDevice_ExpireYear,AbpSession.GetTenantId())
                }
            };

            return await Task.FromResult(output);
        }

        public async Task UpdateTenantSettingAsync(UpdateTenantSettingInput input)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), SpartanSettingKeys.Tenant_Customer_InstallDevice_ExpireYear, input.TenantSetting.Tenant_Customer_InstallDevice_ExpireYear);
        }

        public async Task<GetUpdateUserSettingOutput> GetUpdateUserSettingAsync()
        {
            var output = new GetUpdateUserSettingOutput
            {
                UserSetting = new UserSettingDto
                {
                    User_IsEnableAlarm = SettingManager.GetSettingValueForUser<bool>(SpartanSettingKeys.User_IsEnableAlarm, AbpSession.GetTenantId(),AbpSession.GetUserId()),
                    User_IsSendEmail = SettingManager.GetSettingValueForUser<bool>(SpartanSettingKeys.User_IsSendEmail, AbpSession.GetTenantId(),AbpSession.GetUserId()),
                    User_IsSendApp = SettingManager.GetSettingValueForUser<bool>(SpartanSettingKeys.User_IsSendApp, AbpSession.GetTenantId(),AbpSession.GetUserId()),
                    User_ReceiveEmails = SettingManager.GetSettingValueForUser(SpartanSettingKeys.User_ReceiveEmails, AbpSession.GetTenantId(),AbpSession.GetUserId()),
                    User_ReceiveStartTime = SettingManager.GetSettingValueForUser(SpartanSettingKeys.User_ReceiveStartTime, AbpSession.GetTenantId(),AbpSession.GetUserId()),
                    User_ReceiveEndTime = SettingManager.GetSettingValueForUser(SpartanSettingKeys.User_ReceiveEndTime, AbpSession.GetTenantId(),AbpSession.GetUserId()),
                    User_FortifyRadius = SettingManager.GetSettingValueForUser(SpartanSettingKeys.User_FortifyRadius, AbpSession.GetTenantId(),AbpSession.GetUserId())
                }
            };

            return await Task.FromResult(output);
        }

        public async Task UpdateUserSettingAsync(UpdateUserSettingInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.GetUserId(),SpartanSettingKeys.User_IsEnableAlarm, input.UserSetting.User_IsEnableAlarm.ToString());
            await SettingManager.ChangeSettingForUserAsync(AbpSession.GetUserId(),SpartanSettingKeys.User_IsSendEmail, input.UserSetting.User_IsSendEmail.ToString());
            await SettingManager.ChangeSettingForUserAsync(AbpSession.GetUserId(),SpartanSettingKeys.User_IsSendApp, input.UserSetting.User_IsSendApp.ToString());
            await SettingManager.ChangeSettingForUserAsync(AbpSession.GetUserId(),SpartanSettingKeys.User_ReceiveEmails, input.UserSetting.User_ReceiveEmails);
            await SettingManager.ChangeSettingForUserAsync(AbpSession.GetUserId(),SpartanSettingKeys.User_ReceiveStartTime, input.UserSetting.User_ReceiveStartTime);
            await SettingManager.ChangeSettingForUserAsync(AbpSession.GetUserId(),SpartanSettingKeys.User_ReceiveEndTime, input.UserSetting.User_ReceiveEndTime);
            await SettingManager.ChangeSettingForUserAsync(AbpSession.GetUserId(),SpartanSettingKeys.User_FortifyRadius, input.UserSetting.User_FortifyRadius);

        }
    }
}
