using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Timing;
using Abp.Web.Mvc.Authorization;
using CH.Spartan.Devices;
using CH.Spartan.Devices.Dto;
using CH.Spartan.Sessions;
using CH.Spartan.Web.Models;

namespace CH.Spartan.Web.Controllers
{

    [AbpMvcAuthorize]
    public class HomeController : SpartanControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IDeviceAppService _deviceAppService;

        public HomeController(
            IUserNavigationManager userNavigationManager,
            ILocalizationManager localizationManager,
            ISessionAppService sessionAppService, IDeviceAppService deviceAppService)
        {
            _userNavigationManager = userNavigationManager;
            _localizationManager = localizationManager;
            _sessionAppService = sessionAppService;
            _deviceAppService = deviceAppService;
        }
        public async Task<ActionResult> Index()
        {
            var model = new IndexViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.UserId)),
                CurrentLanguage = _localizationManager.CurrentLanguage,
                Languages = _localizationManager.GetAllLanguages(),
                ShowUpdateDeviceUrl = AbpSession.IsTenantAdmin()? "/AgentManage/UpdateDevice" : "/CustomerManage/UpdateDevice",
                IsTenantAdmin = AbpSession.IsTenantAdmin(),
                LastDevices =
                    await _deviceAppService.GetLastDeviceListAsync(new GetLastDeviceListInput
                    {
                        UserId = AbpSession.GetUserIdIfNotTenantAdmin(),
                        StartTime = DateTimeRange.Last30Days.StartTime,
                        EndTime = DateTimeRange.Last30Days.EndTime
                    }),
                NearExpireDevices =
                    await _deviceAppService.GetNearExpireDeviceListAsync(new GetNearExpireDeviceListInput
                    {
                        MaxResultCount = 10,
                        UserId = AbpSession.GetUserIdIfNotTenantAdmin()
                    })
            };

            if (AbpSession.UserId.HasValue)
            {
                model.LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations());
            }
            return View(model);
        }

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}