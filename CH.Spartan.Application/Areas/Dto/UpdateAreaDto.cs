using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CH.Spartan.Infrastructure;
namespace CH.Spartan.Areas.Dto
{
    [AutoMap(typeof(Area))]
    public class UpdateAreaDto : EntityDto, IDoubleWayDto
    {
        /// <summary>
        /// 区域名字
        /// </summary>
        [MaxLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// 区域点集合
        /// </summary>
        public string Points { get; set; }

    }

    public class UpdateAreaInput : IInputDto
    {
        public UpdateAreaDto Area { get; set; }

        public EnumCoordinates Coordinates { get; set; }
    }

    public class UpdateAreaOutput : IOutputDto
    {
        public UpdateAreaOutput(UpdateAreaDto area)
        {
            Area = area;
        }

        public UpdateAreaDto Area { get; set; }
    }
}
