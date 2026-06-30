namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BlacklistEntry
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime BannedUntil { get; set; }

        /// <summary>
        /// Name, Defaultasplayer
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// PlayerId（IdOrcross-platformId, ：Type + Id,  EOS_XXXX Or Steam_XXXX）
        /// </summary>
        public string PlayerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Reason { get; set; }
    }
}