using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;

namespace CH.Spartan.HistoryDatas.Dto
{
    #region Common

    public class GetHistoryDataDto : IDto
    {

    }

    public class GetHistoryDataSectionDto : IDto
    {

    }

    public class GetHistoryDataInput : IInputDto
    {
        [Range(1, int.MaxValue)]
        public int DeviceId { get; set; }
    }

    public class GetHistoryDataOutput<T> : ListResultOutput<T>
    {
        public List<GetHistoryDataSectionDto> Sections { get; set; }
    }

    #endregion


    #region Web

    [AutoMap(typeof(HistoryData))]
    public class GetHistoryDataForWebDto : GetHistoryDataDto
    {

    }



    public class GetHistoryDataForWebInput : GetHistoryDataInput
    {

    }


    public class GetHistoryDataForWebOutput : GetHistoryDataOutput<GetHistoryDataForWebDto>
    {

    } 
    #endregion

}
