namespace SdtdMultiServerKit.Models
{
    public class PrivateMessage : GlobalMessage
    {
        /// <summary>
        /// TargetplayerIdOr
        /// </summary>
        public required string TargetPlayerIdOrName { get; set; }
    }
}