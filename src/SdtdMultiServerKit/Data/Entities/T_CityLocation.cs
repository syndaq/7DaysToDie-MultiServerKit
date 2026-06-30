using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    /// <summary>
    /// City location
    /// </summary>
    [Table("T_CityLocation_v1")]
    public class T_CityLocation
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
        /// City name
        /// </summary>
        public required string CityName { get; set; }

        /// <summary>
        /// Points required for teleport
        /// </summary>
        public int PointsRequired { get; set; }

        /// <summary>
        /// 3D coordinates
        /// </summary>
        public required string Position { get; set; }

        /// <summary>
        /// View direction
        /// </summary>
        public string? ViewDirection { get; set; }
    }
}