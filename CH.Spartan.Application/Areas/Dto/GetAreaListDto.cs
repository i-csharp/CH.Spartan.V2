using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CH.Spartan.Infrastructure;
namespace CH.Spartan.Areas.Dto
{
    [AutoMapFrom(typeof(Area))]
    public class GetAreaListDto : AuditedEntityDto
    {
        public GetAreaListDto()
        {
            var code = Guid.NewGuid().GetHashCode();
            if (code % 4 == 0)
            {
                Cls = "warning-element";
            }
            else if(code % 3 == 0)
            {
                Cls = "success-element";
            }
            else if (code % 2 == 0)
            {
                Cls = "danger-element";
            }
            else if (code % 1 == 0)
            {
                Cls = "info-element";
            }

        }
        /// <summary>
        /// 区域名字
        /// </summary>
        [MaxLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// 区域点集合
        /// </summary>
        [MaxLength(500)]
        public string Points { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string Cls { get; set; }

    }

    public class GetAreaListInput : QueryListResultRequestInput
    {
        public EnumCoordinates Coordinates { get; set; }
    }

    public class GetAreaListPagedInput : QueryListPagedResultRequestInput
    {

    }
}
