﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Models;
using Abp.Web.Mvc.Models;
using Abp.WebApi.Authorization;
using CH.Spartan.MultiTenancy;
using CH.Spartan.MultiTenancy.Dto;

namespace CH.Spartan.Web.Controllers
{
    [AbpApiAuthorize]
    public class SystemManageController : SpartanControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public SystemManageController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Tenant()
        {
            return View();
        }

        public async Task<JsonResult> TenantSearch(GetTenantsInput input)
        {
            var result = await _tenantAppService.GetTenants(input);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}