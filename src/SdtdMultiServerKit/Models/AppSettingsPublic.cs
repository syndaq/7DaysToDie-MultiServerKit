namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Public view of application settings (secrets omitted).
    /// </summary>
    public class AppSettingsPublic
    {
        public bool ApiOnly { get; set; }
        public string ServerId { get; set; } = string.Empty;
        public bool EnableSwagger { get; set; }
        public string WebUrl { get; set; } = string.Empty;
        public int WebSocketPort { get; set; }
        public string WebSocketUrl { get; set; } = string.Empty;
        public int AccessTokenExpireTime { get; set; }
        public string DatabasePath { get; set; } = string.Empty;
        public string ServerSettingsFileName { get; set; } = string.Empty;

        public static AppSettingsPublic FromAppSettings(AppSettings settings)
        {
            return new AppSettingsPublic
            {
                ApiOnly = settings.ApiOnly,
                ServerId = settings.ServerId,
                EnableSwagger = settings.EnableSwagger,
                WebUrl = settings.WebUrl,
                WebSocketPort = settings.WebSocketPort,
                WebSocketUrl = settings.WebSocketUrl,
                AccessTokenExpireTime = settings.AccessTokenExpireTime,
                DatabasePath = settings.DatabasePath,
                ServerSettingsFileName = settings.ServerSettingsFileName,
            };
        }
    }
}
