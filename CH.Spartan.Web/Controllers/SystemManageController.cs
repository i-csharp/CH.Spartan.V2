using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using Abp.WebApi.Authorization;
using CH.Spartan.AuditLogs;
using CH.Spartan.AuditLogs.Dto;
using CH.Spartan.Authorization;
using CH.Spartan.Devices;
using CH.Spartan.DeviceTypes;
using CH.Spartan.DeviceTypes.Dto;
using CH.Spartan.Infrastructure;
using CH.Spartan.Tenants;
using CH.Spartan.Tenants.Dto;
using CH.Spartan.Nodes;
using CH.Spartan.Nodes.Dto;
using CH.Spartan.Settings;
using CH.Spartan.Settings.Dto;
using CH.Spartan.Users;
using CH.Spartan.Users.Dto;

namespace CH.Spartan.Web.Controllers
{

    /// <summary>
    /// 系统设置
    /// </summary>
    [AbpMvcAuthorize]
    public class SystemManageController : SpartanControllerBase
    {
        private readonly IAuditLogAppService _auditLogAppService;
        private readonly ITenantAppService _tenantAppService;
        private readonly IDeviceTypeAppService _deviceTypeAppService;
        private readonly INodeAppService _nodeAppService;
        private readonly IUserAppService _userAppService;
        private readonly ISettingAppService _settingAppService;
        public SystemManageController(
            ITenantAppService tenantAppService, 
            IDeviceTypeAppService deviceTypeAppService, 
            INodeAppService nodeAppService, 
            IAuditLogAppService auditLogAppService, 
            IUserAppService userAppService,
            ISettingAppService settingAppService)
        {
            _tenantAppService = tenantAppService;
            _deviceTypeAppService = deviceTypeAppService;
            _nodeAppService = nodeAppService;
            _auditLogAppService = auditLogAppService;
            _userAppService = userAppService;
            _settingAppService = settingAppService;
        }

        #region 租户

        #region 首页
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Tenant)]
        public ActionResult Tenant()
        {
            return View();
        }
        #endregion

        #region 查询

        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Tenant)]
        public async Task<JsonResult> GetTenantListPaged(GetTenantListPagedInput input)
        {
            var result = await _tenantAppService.GetTenantListPagedAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加
        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Tenant_Create)]
        public ActionResult CreateTenant()
        {
            var result = _tenantAppService.GetNewTenant();
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Tenant_Create)]
        public async Task<JsonResult> CreateTenant(CreateTenantInput input)
        {
            await _tenantAppService.CreateTenantAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 更新
        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Tenant_Update)]
        public async Task<ActionResult> UpdateTenant(IdInput input)
        {
            var result = await _tenantAppService.GetUpdateTenantAsync(input);
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Tenant_Update)]
        public async Task<JsonResult> UpdateTenant(UpdateTenantInput input)
        {
            await _tenantAppService.UpdateTenantAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除
        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Tenant_Delete)]
        public async Task<JsonResult> DeleteTenant(List<IdInput> input)
        {
            await _tenantAppService.DeleteTenantAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #endregion

        #region 设备类型

        #region 首页
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_DeviceType)]
        public ActionResult DeviceType()
        {
            return View();
        }
        #endregion

        #region 查询

        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_DeviceType)]
        public async Task<JsonResult> GetDeviceTypeListPaged(GetDeviceTypeListPagedInput input)
        {
            var result = await _deviceTypeAppService.GetDeviceTypeListPagedAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 更新
        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_DeviceType_Update)]
        public async Task<ActionResult> UpdateDeviceType(IdInput input)
        {
            var result = await _deviceTypeAppService.GetUpdateDeviceTypeAsync(input);
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_DeviceType_Update)]
        public async Task<JsonResult> UpdateDeviceType(UpdateDeviceTypeInput input)
        {
            await _deviceTypeAppService.UpdateDeviceTypeAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region 节点

        #region 首页
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Node)]
        public ActionResult Node()
        {
            return View();
        }
        #endregion

        #region 查询

        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Node)]
        public async Task<JsonResult> GetNodeListPaged(GetNodeListPagedInput input)
        {
            var result = await _nodeAppService.GetNodeListPagedAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加
        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Node_Create)]
        public ActionResult CreateNode()
        {
            var result = _nodeAppService.GetNewNode();
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Node_Create)]
        public async Task<JsonResult> CreateNode(CreateNodeInput input)
        {
            await _nodeAppService.CreateNodeAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 更新
        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Node_Update)]
        public async Task<ActionResult> UpdateNode(IdInput input)
        {
            var result = await _nodeAppService.GetUpdateNodeAsync(input);
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Node_Update)]
        public async Task<JsonResult> UpdateNode(UpdateNodeInput input)
        {
            await _nodeAppService.UpdateNodeAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #endregion

        #region 审计日志

        #region 首页
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_AuditLog)]
        public ActionResult AuditLog()
        {
            return View();
        }
        #endregion

        #region 查询

        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_AuditLog)]
        public async Task<JsonResult> GetAuditLogListPaged(GetAuditLogListPagedInput input)
        {
            var result = await _auditLogAppService.GetAuditLogListPagedAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除
        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_AuditLog_Delete)]
        public async Task<JsonResult> DeleteAuditLog(List<IdInput> input)
        {
            await _auditLogAppService.DeleteAuditLogAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #endregion

        #region 个人
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_UserInfo)]
        public async Task<ActionResult> UserInfo()
        {
            var result = await _userAppService.GetUpdateUserInfoAsync(new IdInput<long>(AbpSession.GetUserId()));
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_UserInfo)]
        public async Task<JsonResult> UpdateUserInfo(UpdateUserInfoInput input)
        {
            var result = await _userAppService.UpdateUserInfoAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_ChangePassword)]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_ChangePassword)]
        public async Task<JsonResult> ChangePassword(ChangePasswordInput input)
        {
            await _userAppService.ChangePasswordAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 设置

        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Setting)]
        public async Task<ActionResult> Setting()
        {
            var result = await _settingAppService.GetUpdateGeneralSettingAsync();
            return View(result);
        }

        [AbpMvcAuthorize(SpartanPermissionNames.SystemManages_Setting)]
        public async Task<ActionResult> UpdateSetting(UpdateGeneralSettingInput input)
        {
            await _settingAppService.UpdateGeneralSettingAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}