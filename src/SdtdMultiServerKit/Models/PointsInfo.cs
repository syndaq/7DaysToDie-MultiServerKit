namespace SdtdMultiServerKit.Models
{
    public class PointsInfo
    {
        /// <summary>
        /// PlayerId
        /// </summary>
        public required string Id { get; set; }

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
        public DateTime LastSignInAt { get; set; }
    }
}
