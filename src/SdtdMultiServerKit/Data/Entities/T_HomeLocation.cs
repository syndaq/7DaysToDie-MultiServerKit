using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    /// <summary>
    /// Homelocation
    /// </summary>
    public class T_HomeLocation
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
        /// HomeName
        /// </summary>
        public required string HomeName { get; set; }

        /// <summary>
        /// 3D coordinates
        /// </summary>
        public required string Position { get; set; }
    }
}