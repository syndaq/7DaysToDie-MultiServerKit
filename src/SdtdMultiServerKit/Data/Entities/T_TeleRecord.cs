using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    /// <summary>
    /// Teleport record
    /// </summary>
    public class T_TeleRecord
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [PrimaryKey, IgnoreUpdate, IgnoreInsert]
        public int Id { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        [IgnoreUpdate]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// PlayerId
        /// </summary>
        public required string PlayerId { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        public required string PlayerName { get; set; }

        /// <summary>
        /// Targettype
        /// </summary>
        public required string TargetType { get; set; }

        /// <summary>
        /// Target name
        /// </summary>
        public required string TargetName { get; set; }

        /// <summary>
        /// Coordinates, 
        /// </summary>
        public required string OriginPosition { get; set; }

        /// <summary>
        /// Coordinates, 
        /// </summary>
        public required string TargetPosition { get; set; }
    }
}