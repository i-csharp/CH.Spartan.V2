using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Extensions;
using System.Data.Entity;
using Abp.Json;
using Abp.Runtime.Session;
using CH.Spartan.Areas.Dto;
using CH.Spartan.Infrastructure;
using CH.Spartan.Maps;

namespace CH.Spartan.Areas
{

    public class AreaAppService : SpartanAppServiceBase, IAreaAppService
    {
        private readonly AreaManager _areaManager;
        private readonly MapManager _mapManager;
        private readonly IRepository<Area> _areaRepository;
        public AreaAppService(IRepository<Area> areaRepository, AreaManager areaManager, MapManager mapManager)
        {
            _areaRepository = areaRepository;
            _areaManager = areaManager;
            _mapManager = mapManager;
        }

        public async Task<GetAreaOutput> GetAreaAsync(IdInput input)
        {
            var result = await _areaRepository.GetAsync(input.Id);
            return new GetAreaOutput(result.MapTo<GetAreaDto>());
        }

        public async Task<ListResultOutput<GetAreaListDto>> GetAreaListAsync(GetAreaListInput input)
        {
            var list = await _areaRepository.GetAll()
                .WhereIf(!input.SearchText.IsNullOrEmpty(), p => p.Name.Contains(input.SearchText))
                .WhereIf(input.UserId.HasValue,p=>p.UserId==input.UserId)
                .OrderBy(input)
                .Take(input)
                .ToListAsync();
            switch (input.Coordinates)
            {
                case EnumCoordinates.Wgs84:
                    break;
                case EnumCoordinates.Gcj02:
                    foreach (var item in list)
                    {
                        item.Points = _mapManager.Wgs84ToGcj02(item.Points.ToObject<IList<MapPoint>>().ToArray()).ToJsonString();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return new ListResultOutput<GetAreaListDto>(list.MapTo<List<GetAreaListDto>>());
        }

        public async Task<ListResultOutput<ComboboxItemDto>> GetAreaListAutoCompleteAsync(GetAreaListInput input)
        {
            var list = await _areaRepository.GetAll().WhereIf(!input.SearchText.IsNullOrEmpty(), p => p.Name.Contains(input.SearchText)).OrderBy(input).Take(input).ToListAsync();

            return new ListResultOutput<ComboboxItemDto>(list.Select(p => new ComboboxItemDto {Value = p.Id.ToString(), DisplayText = p.Name}).ToList());
        }

        public async Task<GetAreaOutput> CreateAreaAsync(CreateAreaInput input)
        {
            switch (input.Coordinates)
            {
                case EnumCoordinates.Wgs84:
                    break;
                case EnumCoordinates.Gcj02:
                    input.Area.Points = _mapManager.Gcj02ToWgs84(input.Area.Points.ToObject<IList<MapPoint>>().ToArray()).ToJsonString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var area = input.Area.MapTo<Area>();
            area.UserId = AbpSession.GetUserId();
            await _areaRepository.InsertAsync(area);
            UnitOfWorkManager.Current.SaveChanges();
            return new GetAreaOutput(area.MapTo<GetAreaDto>());
        }

        public async Task<GetAreaOutput> UpdateAreaAsync(UpdateAreaInput input)
        {
            switch (input.Coordinates)
            {
                case EnumCoordinates.Wgs84:
                    break;
                case EnumCoordinates.Gcj02:
                    input.Area.Points = _mapManager.Gcj02ToWgs84(input.Area.Points.ToObject<IList<MapPoint>>().ToArray()).ToJsonString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var area = await _areaRepository.GetAsync(input.Area.Id);
            input.Area.MapTo(area);
            await _areaRepository.UpdateAsync(area);
            return new GetAreaOutput(area.MapTo<GetAreaDto>());
        }

        public async Task DeleteAreaAsync(List<IdInput> input)
        {
            await _areaManager.DeleteByIdsAsync(input.Select(p => p.Id));
        }
    }
}