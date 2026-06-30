using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdtdMultiServerKit.Models
{
    public class AvailablePrefabQuery
    {
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Count,  0 Whenreturnallrecord
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(Language.English)]
        public Language Language { get; set; } = Language.English;

        /// <summary>
        /// 
        /// </summary>
        public string? Keyword { get; set; }
    }
}
