namespace SdtdMultiServerKit
{
    internal static class AppSettingsListenUrl
    {
        internal static string Resolve(string configuredWebUrl, bool apiOnly)
        {
            if (string.IsNullOrWhiteSpace(configuredWebUrl))
            {
                configuredWebUrl = "http://127.0.0.1:8888";
            }

            if (!Uri.TryCreate(configuredWebUrl, UriKind.Absolute, out Uri? uri))
            {
                return configuredWebUrl;
            }

            if (apiOnly && IsLocalHost(uri.Host))
            {
                return $"{uri.Scheme}://+:{uri.Port}/";
            }

            return configuredWebUrl.EndsWith("/", StringComparison.Ordinal)
                ? configuredWebUrl
                : configuredWebUrl + "/";
        }

        internal static void WarnIfPanelUnreachable(AppSettings settings)
        {
            if (!settings.ApiOnly || string.IsNullOrWhiteSpace(settings.PanelUrl))
            {
                return;
            }

            if (!Uri.TryCreate(settings.WebUrl, UriKind.Absolute, out Uri? uri))
            {
                return;
            }

            if (IsLocalHost(uri.Host))
            {
                CustomLogger.Warn(
                    "WebUrl is localhost but PanelUrl is set. External panels cannot reach 127.0.0.1 — "
                    + "set WebUrl to your public IP (e.g. http://107.172.208.4:8888) in Mod/LSTY_Data/appsettings.json "
                    + "or delete override files so Config/appsettings.json applies, then restart.");
            }
        }

        private static bool IsLocalHost(string host) =>
            host.Equals("127.0.0.1", StringComparison.OrdinalIgnoreCase)
            || host.Equals("localhost", StringComparison.OrdinalIgnoreCase);
    }
}
