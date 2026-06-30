using System.ComponentModel;

namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Itemblockparameter
    /// </summary>
    public class ItemBlockQuery
    {
        /// <summary>
        /// Itemblockclass
        /// </summary>
        [DefaultValue(ItemBlockKind.All)]
        public ItemBlockKind ItemBlockKind { get; set; } = ItemBlockKind.All;

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

        /// <summary>
        /// Whether to show developer item blocks
        /// </summary>
        [DefaultValue(false)]
        public bool ShowUserHidden { get; set; }
    }
}