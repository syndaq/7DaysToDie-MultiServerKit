namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Goods
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// GoodsId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// GoodsName
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }
    }
}
