﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using CH.Spartan.Users;

namespace CH.Spartan.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AutoCompleteController : Controller
    {
        private readonly IUserAppService _userAppService;

        public AutoCompleteController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public ActionResult User(string q)
        {
            return View();
        }
    }
}