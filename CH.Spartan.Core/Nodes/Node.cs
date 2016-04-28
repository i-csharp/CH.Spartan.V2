using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace CH.Spartan.Nodes
{
    public class Node : FullAuditedEntity
    {

        /// <summary>
        /// 节点名字
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 历史数据表
        /// </summary>
        [MaxLength(100)]
        public string HistoryTableName { get; set; }

        /// <summary>
        /// 历史数据表 读字符串
        /// </summary>
        [MaxLength(250)]
        public string HistoryConnectionStringRead { get; set; }

        /// <summary>
        /// 历史数据表 写字符串
        /// </summary>
        [MaxLength(250)]
        public string HistoryConnectionStringWrite { get; set; }

        /// <summary>
        /// 使用量
        /// </summary>
        public int UsageCount { get; set; }
    }
}
