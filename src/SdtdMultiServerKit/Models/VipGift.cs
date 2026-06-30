namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class VipGift
    {
        /// <summary>
        /// PlayerId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public bool ClaimState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalClaimCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }
    }
}
