using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using CH.Spartan.Devices;
using CH.Spartan.Devices.Dto;
using CH.Spartan.MultiTenancy;
using CH.Spartan.MultiTenancy.Dto;
using CH.Spartan.Users;
using CH.Spartan.Users.Dto;
using CH.Spartan.Web.Models;

namespace CH.Spartan.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AutoCompleteController : SpartanControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ITenantAppService _tenantAppService;
        private readonly IDeviceAppService _deviceAppService;
        public AutoCompleteController(IUserAppService userAppService, ITenantAppService tenantAppService, IDeviceAppService deviceAppService)
        {
            _userAppService = userAppService;
            _tenantAppService = tenantAppService;
            _deviceAppService = deviceAppService;
        }
        public async Task<JsonResult> GetTenantList(GetTenantListInput input)
        {
            var result = await _tenantAppService.GetTenantListAutoCompleteAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetUserList(GetUserListInput input)
        {
            var result = await _userAppService.GetUserListAutoCompleteAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDeviceList(GetDeviceListInput input)
        {
            var result = await _deviceAppService.GetDeviceListAutoCompleteAsync(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}