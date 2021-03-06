﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CH.Spartan.Authorization.Roles.Dto;
using CH.Spartan.Roles.Dto;

namespace CH.Spartan.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);

        /// <summary>
        /// 获取 角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<GetRoleListDto>> GetRoleListAsync(GetRoleListInput input);

        /// <summary>
        /// 获取 角色 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<GetRoleListDto>> GetRoleListPagedAsync(GetRoleListPagedInput input);

        /// <summary>
        /// 添加 角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateRoleAsync(CreateRoleInput input);

        /// <summary>
        /// 更新 角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateRoleAsync(UpdateRoleInput input);

        /// <summary>
        /// 获取 新角色
        /// </summary>
        /// <returns></returns>
        CreateRoleOutput GetNewRole();

        /// <summary>
        /// 获取 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UpdateRoleOutput> GetUpdateRoleAsync(IdInput input);

        /// <summary>
        /// 删除 角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRoleAsync(List<IdInput> input);
    }
}
