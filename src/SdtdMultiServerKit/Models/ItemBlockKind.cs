namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Itemblockclass
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ItemBlockKind
    {
        /// <summary>
        /// 
        /// </summary>
        All,

        /// <summary>
        /// Item
        /// </summary>
        Item,

        /// <summary>
        /// Block
        /// </summary>
        Block,
    }
}