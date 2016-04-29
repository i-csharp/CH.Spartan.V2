using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CH.Spartan.Users.Dto
{
    public class ChangePasswordInput : IInputDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword1 { get; set; }

        [Required]
        public string NewPassword2 { get; set; }
    }
}
