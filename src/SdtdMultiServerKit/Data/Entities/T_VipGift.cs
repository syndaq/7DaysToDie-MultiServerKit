using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    /// <summary>
    /// Goods
    /// </summary>
    [Table("T_VipGift_v1")]
    public class T_VipGift
    {
        /// <summary>
        /// PlayerId
        /// </summary>
        [PrimaryKey]
        public string Id { get; set; }

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
        /// State, true: , false: 
        /// </summary>
        public bool ClaimState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalClaimCount { get; set; }

        /// <summary>
        /// Afterdate
        /// </summary>
        public DateTime? LastClaimAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }
    }
}