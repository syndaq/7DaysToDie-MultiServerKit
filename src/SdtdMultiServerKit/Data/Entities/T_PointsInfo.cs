using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    /// <summary>
    /// Points info
    /// </summary>
    [Table("T_PointsInfo_v1")]
    public class T_PointsInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [PrimaryKey]
        public required string Id { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        public string? PlayerName { get; set; }

        /// <summary>
        /// Points balance
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Last sign-in date
        /// </summary>
        public DateTime? LastSignInAt { get; set; }
    }
}
