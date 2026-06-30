namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChatType
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Global
        /// </summary>
        Global = 0,

        /// <summary>
        /// Friend
        /// </summary>
        Friends = 1,

        /// <summary>
        /// 
        /// </summary>
        Party = 2,

        /// <summary>
        /// 
        /// </summary>
        Whisper = 3,
    }
}