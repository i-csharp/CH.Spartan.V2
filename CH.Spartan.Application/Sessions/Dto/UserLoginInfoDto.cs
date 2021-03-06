﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CH.Spartan.Users;

namespace CH.Spartan.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }
}
