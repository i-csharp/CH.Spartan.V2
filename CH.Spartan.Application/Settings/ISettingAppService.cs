using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CH.Spartan.Settings.Dto;

namespace CH.Spartan.Settings
{
    public interface ISettingAppService : IApplicationService
    {
        Task<GetUpdateGeneralSettingOutput> GetUpdateGeneralSettingAsync();

        Task UpdateGeneralSettingAsync(UpdateGeneralSettingInput input);

        Task<GetUpdateTenantSettingOutput> GetUpdateTenantSettingAsync();

        Task UpdateTenantSettingAsync(UpdateTenantSettingInput input);

        Task<GetUpdateUserSettingOutput> GetUpdateUserSettingAsync();

        Task UpdateUserSettingAsync(UpdateUserSettingInput input);
    }
    
}
