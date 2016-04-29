using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using CH.Spartan.HistoryDatas.Dto;

namespace CH.Spartan.HistoryDatas
{
    public interface IHistoryDataService : IApplicationService
    {
        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetHistoryDataForWebOutput> GetHistoryDataForWeb(GetHistoryDataForWebInput input);
    }
}
