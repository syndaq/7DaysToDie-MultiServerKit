using System.ComponentModel;

namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Paginationparameter
    /// </summary>
    public class PaginationQuery
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
        public string? Keyword { get; set; }

        /// <summary>
        /// Sort
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// Whether
        /// </summary>
        public bool Desc { get; set; }
    }

    /// <summary>
    /// Paginationparameter
    /// </summary>
    public class PaginationQuery<TOrder> where TOrder: Enum
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
        public string? Keyword { get; set; }

        /// <summary>
        /// Sort
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TOrder? Order { get; set; }

        /// <summary>
        /// Whether
        /// </summary>
        public bool Desc { get; set; }
    }
}