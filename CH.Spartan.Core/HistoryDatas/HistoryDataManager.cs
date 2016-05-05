using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Runtime.Caching;
using CH.Spartan.Devices;
using CH.Spartan.Domain;
using CH.Spartan.Infrastructure;
using CH.Spartan.Maps;
using CH.Spartan.Tenants;
using CH.Spartan.Nodes;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNet.Identity;

namespace CH.Spartan.HistoryDatas
{
    public class HistoryDataManager : ManagerBase
    {
        private readonly MapManager _mapManager;
        public HistoryDataManager(
            ISettingManager settingManager,
            ICacheManager cacheManager,
            IIocResolver iocResolver,
            IUnitOfWorkManager unitOfWorkManager, MapManager mapManager)
            : base(settingManager, cacheManager, iocResolver, unitOfWorkManager)
        {
            _mapManager = mapManager;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="node">节点</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public async Task<List<HistoryData>> GetHistoryDataAsync(Device device, Node node,DateTime? startTime, DateTime? endTime)
        {
            using (IDbConnection conn = new MySqlConnection(node.HistoryConnectionStringRead))
            {
                var cmdText =$"SELECT * FROM `{node.HistoryTableName}` a WHERE a.`DeviceId`={device.Id} AND a.`ReportTime`>'{startTime}' AND a.`ReportTime`<'{endTime}'";
                return (await conn.QueryAsync<HistoryData>(cmdText)).ToList();
            }
        }

        /// <summary>
        /// 计算里程
        /// </summary>
        /// <param name="list">数据集合</param>
        /// <returns></returns>
        public double CalcMileage(List<HistoryData> list)
        {
            var mileage = 0.00;
            for (var i = 1; i < list.Count; i++)
            {
                mileage += _mapManager.GetDistance(new MapPoint(list[i].Latitude, list[i].Longitude), new MapPoint(list[i - 1].Latitude, list[i - 1].Longitude));
                list[i].Mileage = mileage / 1000;
            }
            return Math.Round(mileage / 1000, 2);
        }

        /// <summary>
        /// 转换坐标
        /// </summary>
        /// <param name="list">数据集合</param>
        /// <param name="coordinates">当前数据集合坐标系</param>
        /// <returns></returns>
        public void ConvertCoordinates(List<HistoryData> list, EnumCoordinates coordinates)
        {
            switch (coordinates)
            {
                case EnumCoordinates.Wgs84:
                    break;
                case EnumCoordinates.Gcj02:
                    Parallel.ForEach(list, p =>
                    {
                        var point = _mapManager.Wgs84ToGcj02(new MapPoint(p.Latitude, p.Longitude, coordinates));
                        p.Latitude = point.Lat;
                        p.Longitude = p.Longitude;
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(coordinates), coordinates, null);
            }
        }

        /// <summary>
        /// 分析区间数据
        /// </summary>
        /// <param name="list">数据集合</param>
        /// <param name="parkingTime">停车时长</param>
        public void AnalyzeSections(List<HistoryData> list,int parkingTime)
        {
            for (var i = 0; i < list.Count; i++)
            {
                //区间信息记录分析
                if (i > 0)
                {
                    //计算区间时间点
                    var minutes = Convert.ToInt32((list[i].ReportTime - list[i - 1].ReportTime).TotalMinutes);
                    if (minutes >= parkingTime)
                    {
                        list[i - 1].IsSection = true;
                        var distance = Math.Round(_mapManager.GetDistance(new MapPoint(list[i].Latitude, list[i].Longitude), new MapPoint(list[i - 1].Latitude, list[i - 1].Longitude)) / 1000, 2);
                        if (distance > 2)
                        {
                            //无数据
                            list[i - 1].SectionInfo = "无数据:{0}-{1} 间隔约{2}km,共 {3} ".GetFormat(list[i - 1].ReportTime, list[i].ReportTime, distance, DateTime.Now.AddMinutes(minutes).GetDateDiff(DateTime.Now));
                            list[i - 1].SectionIconUrl = "/Content/img/marker/no_data.png";
                            list[i - 1].SectionType = 1;
                        }
                        else
                        {
                            //正常停车
                            if (list[i - 1].ReportTime.Date == list[i].ReportTime.Date)
                            {
                                list[i - 1].SectionInfo = "停车时间:{0} {1}-{2} 共 {3} ".GetFormat(list[i - 1].ReportTime.ToShortDateString(), list[i - 1].ReportTime.TimeOfDay, list[i].ReportTime.TimeOfDay, DateTime.Now.AddMinutes(minutes).GetDateDiff(DateTime.Now));
                            }
                            else
                            {
                                list[i - 1].SectionInfo = "停车时间:{0}-{1} 共 {2} ".GetFormat(list[i - 1].ReportTime, list[i].ReportTime, DateTime.Now.AddMinutes(minutes).GetDateDiff(DateTime.Now));
                            }
                            list[i - 1].SectionIconUrl = "/Content/img/marker/stop.png";
                            list[i - 1].SectionType = 0;
                        }
                    }
                }
            }
        }

    }
}
