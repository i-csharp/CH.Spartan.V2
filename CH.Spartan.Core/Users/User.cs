﻿using System;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Extensions;
using CH.Spartan.Tenants;
using Microsoft.AspNet.Identity;

namespace CH.Spartan.Users
{
    public class User : AbpUser<Tenant, User>
    {
        #region 基本

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 静态用户
        /// 静态用户不能删除 也不能修改用户名
        /// 一般是某个租户的管理员
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }

        #endregion

        #region 绑定

        /// <summary>
        /// QQ互联唯一Id
        /// </summary>
        [MaxLength(200)]
        public string QQOpenId { get; set; }

        /// <summary>
        /// QQ互联访问口令
        /// </summary>
        [MaxLength(200)]
        public string QQAccessToken { get; set; }


        /// <summary>
        /// QQ互联访问过期时间
        /// </summary>
        public int? QQExpiresIn { get; set; }

        /// <summary>
        /// 微信唯一Id
        /// </summary>
        [MaxLength(200)]
        public string WeiXinUnionId { get; set; }

        /// <summary>
        /// 微信访问口令
        /// </summary>
        [MaxLength(200)]
        public string WeiXinAccessToken { get; set; }


        /// <summary>
        /// 微信访问过期时间
        /// </summary>
        public int? WeiXinExpiresIn { get; set; }


        /// <summary>
        /// 微博唯一Id
        /// </summary>
        [MaxLength(200)]
        public string WeiBoOpenId { get; set; }

        /// <summary>
        /// 微博访问口令
        /// </summary>
        [MaxLength(200)]
        public string WeiBoAccessToken { get; set; }

        /// <summary>
        /// 微博访问过期时间
        /// </summary>
        public int? WeiBoExpiresIn { get; set; } 
        #endregion

        #region 方法

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string userName, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = userName,
                Name = userName,
                Surname = userName,
                IsEmailConfirmed = true,
                EmailAddress = emailAddress,
                IsActive = true,
                IsStatic = true,
                Avatar= "/Content/img/avatar.jpg",
                Password = new Md532PasswordHasher().HashPassword(password)
            };
        }
        public static User CreateTenantUser(int tenantId, string userName, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = userName,
                Name = userName,
                Surname = userName,
                IsEmailConfirmed = true,
                EmailAddress = emailAddress,
                IsActive = true,
                IsStatic = false,
                Avatar = "/Content/img/avatar.jpg",
                Password = new Md532PasswordHasher().HashPassword(password)
            };
        }

        #endregion

    }
}