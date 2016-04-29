using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Spartan.HistoryDatas.Dto;

namespace CH.Spartan.HistoryDatas
{
    public class HistoryDataService : IHistoryDataService
    {
        private readonly HistoryDataManager _historyManager;

        public HistoryDataService(HistoryDataManager historyManager)
        {
            _historyManager = historyManager;
        }

        public async Task<GetHistoryDataForWebOutput> GetHistoryDataForWeb(GetHistoryDataForWebInput input)
        {
            throw new NotImplementedException();
        }
    }
}
