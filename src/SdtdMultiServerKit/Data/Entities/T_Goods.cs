using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    /// <summary>
    /// Goods
    /// </summary>
    [Table("T_Goods_v2")]
    public class T_Goods
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        [IgnoreUpdate]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Name
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