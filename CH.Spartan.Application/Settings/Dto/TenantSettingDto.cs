using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace CH.Spartan.Settings.Dto
{
    public class TenantSettingDto : IValidate
    {
        public string Tenant_Customer_InstallDevice_ExpireYear { get; set; }
    }

    public class UpdateTenantSettingInput : IInputDto
    {
        public TenantSettingDto TenantSetting { get; set; }
    }


    public class GetUpdateTenantSettingOutput : IInputDto
    {
        public TenantSettingDto TenantSetting { get; set; }
    }

    
}
