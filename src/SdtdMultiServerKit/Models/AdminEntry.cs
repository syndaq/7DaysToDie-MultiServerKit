namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminEntry
    {
        /// <summary>
        /// PlayerId（IdOrcross-platformId, ：Type + Id,  EOS_XXXX Or Steam_XXXX）
        /// </summary>
        public string PlayerId { get; set; }

        /// <summary>
        /// Etc
        /// </summary>
        public int PermissionLevel { get; set; }

        /// <summary>
        /// Name, Defaultasplayer
        /// </summary>
        public string DisplayName { get; set; }
    }
}