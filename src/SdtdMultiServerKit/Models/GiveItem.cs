using System.ComponentModel;

namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Item
    /// </summary>
    public class GiveItem
    {
        /// <summary>
        /// Count
        /// </summary>
        [DefaultValue(1)]
        public int Count { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public int? Durability { get; set; }

        /// <summary>
        /// ItemName
        /// </summary>
        public required string ItemName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Quality { get; set; }

        /// <summary>
        /// TargetplayerIdOr
        /// </summary>
        public required string TargetPlayerIdOrName { get; set; }
    }
}