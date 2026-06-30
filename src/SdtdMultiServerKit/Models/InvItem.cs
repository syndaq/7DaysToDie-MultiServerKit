namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Item
    /// </summary>
    public class InvItem
    {
        /// <summary>
        /// ItemName
        /// </summary>
        public required string ItemName { get; set; }

        /// <summary>
        /// LocalizationName
        /// </summary>
        public required string LocalizationName { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Maximumheap
        /// </summary>
        public int MaxStackAllowed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? QualityColor { get; set; }

        /// <summary>
        /// Use
        /// </summary>
        public float UseTimes { get; set; }

        /// <summary>
        /// Maximumuse
        /// </summary>
        public int MaxUseTimes { get; set; }

        /// <summary>
        /// Whether
        /// </summary>
        public bool IsMod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public InvItem?[]? Parts { get; set; }
    }
}
