namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Entityinfo
    /// </summary>
    public class EntityInfo
    {
        /// <summary>
        /// EntityId
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// EntityName, Return entity class name if empty
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Entitytype
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Coordinates
        /// </summary>
        public Position Position { get; set; }
    }

    /// <summary>
    /// Entityinfo
    /// </summary>
    public class EntityInfoEx : EntityInfo
    {
        /// <summary>
        /// PlayerId
        /// </summary>
        public string PlayerId { get; set; }
    }
}
