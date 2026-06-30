namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Itemblock
    /// </summary>
    public class ItemBlock
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Whetherasblock
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// LocalizationName
        /// </summary>
        public string LocalizationName { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string IconName { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string IconColor { get; set; }

        /// <summary>
        /// Maximumheapcount
        /// </summary>
        public int MaxStackAllowed { get; set; }
    }
}