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
        public string Avatar { get; set; }

        /// <summary>
        /// 用户名 hhahh2011
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 名称 陈欢
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
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
