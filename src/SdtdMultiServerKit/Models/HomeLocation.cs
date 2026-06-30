namespace SdtdMultiServerKit.Models
{
    public class HomeLocation
    {
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
