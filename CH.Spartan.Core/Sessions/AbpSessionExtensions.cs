using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Runtime.Session;
using CH.Spartan.Authorization.Roles;
using CH.Spartan.Users;
using Microsoft.AspNet.Identity;

namespace CH.Spartan.Sessions
{
    public static class AbpSessionExtensions
    {
        /// <summary>
        /// 是否为租户管理员
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsTenantAdmin(this IAbpSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new AbpException("Session.UserId is null! Probably, user is not logged in.");
            }

            return IocManager.Instance.Resolve<UserManager>().CheckIsTenantAdminAsync(session.GetUserId()).Result;
        }

        /// <summary>
        /// 获取用户Id 如果不是租户管理员的话
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static long? GetUserIdIfNotTenantAdmin(this IAbpSession session)
        {
            return session.IsTenantAdmin() ? null : session.UserId;
        }
    }
}
