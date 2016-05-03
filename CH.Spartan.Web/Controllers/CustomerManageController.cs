﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using CH.Spartan.Areas;
using CH.Spartan.Areas.Dto;
using CH.Spartan.Devices;
using CH.Spartan.Devices.Dto;
using CH.Spartan.HistoryDatas;
using CH.Spartan.HistoryDatas.Dto;
using CH.Spartan.Infrastructure;
using CH.Spartan.Users;
using CH.Spartan.Users.Dto;

namespace CH.Spartan.Web.Controllers
{
    /// <summary>
    /// 客户管理
    /// </summary>
    [AbpMvcAuthorize]
    public class CustomerManageController : SpartanControllerBase
    {
        private readonly IAreaAppService _areaAppService;
        private readonly IDeviceAppService _deviceAppService;
        private readonly IUserAppService _userAppService;
        private readonly IHistoryDataAppService _historyDataAppService;
        public CustomerManageController(
            IAreaAppService areaAppService,
            IDeviceAppService deviceAppService,
            IUserAppService userAppService,
            IHistoryDataAppService historyDataAppService)
        {
            _areaAppService = areaAppService;
            _deviceAppService = deviceAppService;
            _userAppService = userAppService;
            _historyDataAppService = historyDataAppService;
        }

        #region 区域

        #region 首页
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Area)]
        public ActionResult Area()
        {
            return View();
        }
        #endregion

        #region 查询

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Area)]
        public async Task<JsonResult> GetAreaList(GetAreaListInput input)
        {
            input.UserId = AbpSession.GetUserId();
            var result = await _areaAppService.GetAreaListAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加
       
        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Area_Create)]
        public async Task<JsonResult> CreateArea(CreateAreaInput input)
        {
            var result= await _areaAppService.CreateAreaAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 更新

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Area_Update)]
        public async Task<JsonResult> UpdateArea(UpdateAreaInput input)
        {
            var result = await _areaAppService.UpdateAreaAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 删除
        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Area_Delete)]
        public async Task<JsonResult> DeleteArea(List<IdInput> input)
        {
            await _areaAppService.DeleteAreaAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #endregion

        #region 设备

        #region 首页
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Device)]
        public ActionResult Device()
        {
            return View();
        }
        #endregion

        #region 查询

        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Device)]
        public async Task<JsonResult> GetDeviceListPaged(GetDeviceListPagedInput input)
        {
            input.UserId = AbpSession.GetUserId();
            var result = await _deviceAppService.GetDeviceListPagedAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加
        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Device_Create)]
        public ActionResult CreateDevice()
        {
            var result = _deviceAppService.GetNewDeviceByCustomer();
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Device_Create)]
        public async Task<JsonResult> CreateDevice(CreateDeviceByCustomerInput input)
        {
            await _deviceAppService.CreateDeviceByCustomerAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 更新
        [HttpGet]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Device_Update)]
        public async Task<ActionResult> UpdateDevice(IdInput input)
        {
            var result = await _deviceAppService.GetUpdateDeviceByCustomerAsync(input);
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Device_Update)]
        public async Task<JsonResult> UpdateDevice(UpdateDeviceByCustomerInput input)
        {
            await _deviceAppService.UpdateDeviceByCustomerAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除
        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Device_Delete)]
        public async Task<JsonResult> DeleteDevice(List<IdInput> input)
        {
            await _deviceAppService.DeleteDeviceAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #endregion

        #region 监控

        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Monitor)]
        public ActionResult Monitor()
        {
            return View();
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Monitor)]
        public async Task<JsonResult> GetMonitorData(GetMonitorDataByCutomerForWebInput input)
        {
            input.UserId = AbpSession.GetUserId();
            var result = await _deviceAppService.GetMonitorDataByCutomerForWeb(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 历史

        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_HistoryData)]
        public ActionResult HistoryData()
        {
            return View();
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_HistoryData)]
        public async Task<JsonResult> GetHistoryData(GetHistoryDataForWebInput input)
        {
            var result = await _historyDataAppService.GetHistoryDataForWebAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 个人
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_UserInfo)]
        public async Task<ActionResult> UserInfo()
        {
            var result = await _userAppService.GetUpdateUserInfoAsync(new IdInput<long>(AbpSession.GetUserId()));
            return View(result);
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_UserInfo)]
        public async Task<JsonResult> UpdateUserInfo(UpdateUserInfoInput input)
        {
            var result = await _userAppService.UpdateUserInfoAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_ChangePassword)]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_ChangePassword)]
        public async Task<JsonResult> ChangePassword(ChangePasswordInput input)
        {
            await _userAppService.ChangePasswordAsync(input);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 消息
        [AbpMvcAuthorize(SpartanPermissionNames.CustomerManages_Message)]
        public async Task<ActionResult> Message()
        {
            return View();
        }
        #endregion
    }
}