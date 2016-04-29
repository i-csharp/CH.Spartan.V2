using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CH.Spartan.Users.Dto
{
    [AutoMap(typeof(User))]
    public class UpdateUserInfoDto : EntityDto, IDoubleWayDto
    {
        /// <summary>
        /// 头像
        /// </summary>
        [Required]
        public string Avatar { get; set; }

        /// <summary>
        /// 用户名 hhahh2011
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 名称 陈欢
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        [Required]
        public string EmailAddress { get; set; }

    }

    public class UpdateUserInfoInput : IInputDto
    {
        public UpdateUserInfoDto User { get; set; }
    }

    public class UpdateUserInfoOutput : IOutputDto
    {
        public UpdateUserInfoOutput(UpdateUserInfoDto user)
        {
            User = user;
        }

        public UpdateUserInfoDto User { get; set; }
    }
}
