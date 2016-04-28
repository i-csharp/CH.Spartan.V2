using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CH.Spartan.Users.Dto;

namespace CH.Spartan.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<string> GetTenancyNameAsync(string userName);

        /// <summary>
        /// ��ȡ �û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<GetUserListDto>> GetUserListAsync(GetUserListInput input);

        /// <summary>
        /// ��ȡ �û� ��ҳ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<GetUserListDto>> GetUserListPagedAsync(GetUserListPagedInput input);

        /// <summary>
        /// ��ȡ �û� �Զ���ȫ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<ComboboxItemDto>> GetUserListAutoCompleteAsync(GetUserListInput input);

        /// <summary>
        /// ��� �û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateUserAsync(CreateUserInput input);

        /// <summary>
        /// ���� �û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateUserAsync(UpdateUserInput input);

        /// <summary>
        /// ��ȡ ���û�
        /// </summary>
        /// <returns></returns>
        CreateUserOutput GetNewUser();

        /// <summary>
        /// ��ȡ �����û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UpdateUserOutput> GetUpdateUserAsync(IdInput<long> input);

        /// <summary>
        /// ɾ�� �û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteUserAsync(List<IdInput<long>> input);

        /// <summary>
        /// ���� �û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UpdateUserInfoOutput> UpdateUserInfoAsync(UpdateUserInfoInput input);

        /// <summary>
        /// ��ȡ �����û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UpdateUserInfoOutput> GetUpdateUserInfoAsync(IdInput<long> input);

    }
}