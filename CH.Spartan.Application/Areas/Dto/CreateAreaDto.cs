﻿using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CH.Spartan.Infrastructure;
namespace CH.Spartan.Areas.Dto
{
    [AutoMap(typeof(Area))]
    public class CreateAreaDto : IDoubleWayDto
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

    public class CreateAreaInput : IInputDto
    {
        public CreateAreaDto Area { get; set; }

        public EnumCoordinates Coordinates { get; set; }
    }

    public class CreateAreaOutput : IOutputDto
    {
        public CreateAreaOutput(CreateAreaDto area)
        {
            Area = area;
        }

        public CreateAreaDto Area { get; set; }
    }
}


