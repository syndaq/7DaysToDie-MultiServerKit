namespace SdtdMultiServerKit
{
    /// <summary>
    /// AppSettings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// When true, the mod exposes only a REST API for the central panel (no web UI, OAuth, or Steam login).
        /// </summary>
        public bool ApiOnly { get; set; } = true;

        /// <summary>
        /// Shared secret the central panel uses to authenticate API requests to this game server.
        /// </summary>
        public string PanelApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Unique identifier for this game server instance, used by the central panel.
        /// </summary>
        public string ServerId { get; set; } = string.Empty;

        /// <summary>
        /// Base URL of the central panel API (e.g. http://panel-host:3001).
        /// When set, player points are stored in the panel database and synced locally as a cache.
        /// </summary>
        public string PanelUrl { get; set; } = string.Empty;

        /// <summary>
        /// Whether to expose the Swagger UI endpoint. Recommended false in production.
        /// </summary>
        public bool EnableSwagger { get; set; } = false;

        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Server address
        /// </summary>
        public string WebUrl { get; set; } = string.Empty;
        /// <summary>
        /// WebSocket
        /// </summary>
        public int WebSocketPort { get; set; }
        /// <summary>
        /// WebSocketAddress
        /// </summary>
        public string WebSocketUrl { get; set; } = string.Empty;
        /// <summary>
        /// AccessTokenTotime
        /// </summary>
        public int AccessTokenExpireTime { get; set; }
        /// <summary>
        /// Database path
        /// </summary>
        public string DatabasePath { get; set; } = string.Empty;
        /// <summary>
        /// Server configuration file name
        /// </summary>
        public string ServerSettingsFileName { get; set; } = string.Empty;
    }
}