﻿using Abp.Dependency;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.Infrastructure;
using Microsoft.AspNet.Identity;

namespace CH.Spartan.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class SpartanControllerBase : AbpController
    {
        protected SpartanControllerBase()
        {
            LocalizationSourceName = SpartanConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}