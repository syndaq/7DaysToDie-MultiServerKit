namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LogLevel
    {
        /// <summary>
        /// 
        /// </summary>
        Error,

        /// <summary>
        /// 
        /// </summary>
        Assert,

        /// <summary>
        /// 
        /// </summary>
        Warning,

        /// <summary>
        /// Record
        /// </summary>
        Log,

        /// <summary>
        /// 
        /// </summary>
        Exception
    }
}