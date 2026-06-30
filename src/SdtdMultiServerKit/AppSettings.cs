namespace SdtdMultiServerKit
{
    /// <summary>
    /// AppSettings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Username
        /// </summary>
        public required string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public required string Password { get; set; }
        /// <summary>
        /// Server address
        /// </summary>
        public required string WebUrl { get; set; }
        /// <summary>
        /// WebSocket
        /// </summary>
        public int WebSocketPort { get; set; }
        /// <summary>
        /// WebSocketAddress
        /// </summary>
        public required string WebSocketUrl { get; set; }
        /// <summary>
        /// AccessTokenTotime
        /// </summary>
        public int AccessTokenExpireTime { get; set; }
        /// <summary>
        /// Database path
        /// </summary>
        public required string DatabasePath { get; set; }
        /// <summary>
        /// Server configuration file name
        /// </summary>
        public required string ServerSettingsFileName { get; set; }
    }
}