namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WhitelistEntry
    {
        /// <summary>
        /// Name, Defaultasplayer
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// PlayerId（IdOrcross-platformId, ：Type + Id,  EOS_XXXX Or Steam_XXXX）
        /// </summary>
        public string PlayerId { get; set; }
    }
}