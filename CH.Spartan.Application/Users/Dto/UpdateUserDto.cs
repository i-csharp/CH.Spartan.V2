using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CH.Spartan.Users.Dto
{
    [AutoMap(typeof(User))]
    public class UpdateUserDto : EntityDto, IDoubleWayDto
    {
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

        /// <summary>
        /// 联系方式
        /// </summary>
        [Required]
        public string Contact { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

    }

    public class UpdateUserInput : IInputDto
    {
        public UpdateUserDto User { get; set; }
    }

    public class UpdateUserOutput : IOutputDto
    {
        public UpdateUserOutput(UpdateUserDto user)
        {
            User = user;
        }

        public UpdateUserDto User { get; set; }
    }
}
