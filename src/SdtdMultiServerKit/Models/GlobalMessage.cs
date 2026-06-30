namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Globalmessage
    /// </summary>
    public class GlobalMessage
    {
        /// <summary>
        /// Message
        /// </summary>
        public required string Message { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string? SenderName { get; set; }
    }
}