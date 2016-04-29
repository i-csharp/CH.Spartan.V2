using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using CH.Spartan.Devices;
using CH.Spartan.HistoryDatas.Dto;
using CH.Spartan.Maps;
using CH.Spartan.Nodes;
using Dapper;
using MySql.Data.MySqlClient;

namespace CH.Spartan.HistoryDatas
{
    public class HistoryDataAppService :SpartanAppServiceBase, IHistoryDataAppService
    {
        private readonly HistoryDataManager _historyManager;
        private readonly IRepository<Device> _deviceRepository;
        private readonly IRepository<Node> _nodeRepository;
        public HistoryDataAppService(HistoryDataManager historyManager, IRepository<Device> deviceRepository, IRepository<Node> nodeRepository)
        {
            _historyManager = historyManager;
            _deviceRepository = deviceRepository;
            _nodeRepository = nodeRepository;
        }

        //关闭EF 使用Dapper
        [UnitOfWork(IsDisabled = true)]
        public async Task<GetHistoryDataForWebOutput> GetHistoryDataForWebAsync(GetHistoryDataForWebInput input)
        {
            var output=new GetHistoryDataForWebOutput();
            var device = await _deviceRepository.GetAsync(input.DeviceId);
            var node = await _nodeRepository.GetAsync(device.BNodeId);
            //获取数据
            var result = await _historyManager.GetHistoryDataAsync(device, node, input.StartTime, input.EndTime);
            //计算里程
            output.TotalMileage = _historyManager.CalcMileage(result);
            //转换坐标
            _historyManager.ConvertCoordinates(result, input.Coordinates);
            //分析区间
            _historyManager.AnalyzeSections(result,input.ParkingTime);
            output.Items = result.MapTo<List<GetHistoryDataForWebDto>>();
            return output;
        }
    }
}
