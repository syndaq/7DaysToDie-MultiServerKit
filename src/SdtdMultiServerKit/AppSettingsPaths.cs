namespace SdtdMultiServerKit
{
    internal static class AppSettingsPaths
    {
        internal const string LstyDataFolder = "LSTY_Data";
        internal const string AppSettingsFileName = "appsettings.json";

        internal static string DefaultConfigPath(string modPath) =>
            Path.Combine(modPath, "Config", AppSettingsFileName);

        /// <summary>
        /// Preferred runtime override path (documented location under the mod folder).
        /// </summary>
        internal static string ModProductionConfigPath(string modPath) =>
            Path.Combine(modPath, LstyDataFolder, AppSettingsFileName);

        /// <summary>
        /// Legacy runtime override path used by older builds (Unity Managed folder).
        /// </summary>
        internal static string LegacyProductionConfigPath() =>
            Path.Combine(AppContext.BaseDirectory, LstyDataFolder, AppSettingsFileName);

        internal static void EnsureModProductionConfig(string modPath, string defaultConfigPath)
        {
            string productionPath = ModProductionConfigPath(modPath);
            if (File.Exists(productionPath))
            {
                return;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(productionPath)!);
                File.Copy(defaultConfigPath, productionPath);
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, $"Copy appsettings to {productionPath} failed.");
            }
        }

        internal static void PersistAppSettings(string modPath, string json)
        {
            string defaultPath = DefaultConfigPath(modPath);
            File.WriteAllText(defaultPath, json, System.Text.Encoding.UTF8);

            string modProductionPath = ModProductionConfigPath(modPath);
            Directory.CreateDirectory(Path.GetDirectoryName(modProductionPath)!);
            File.WriteAllText(modProductionPath, json, System.Text.Encoding.UTF8);

            string legacyProductionPath = LegacyProductionConfigPath();
            try
            {
                if (File.Exists(legacyProductionPath)
                    || Directory.Exists(Path.GetDirectoryName(legacyProductionPath)))
                {
                    File.WriteAllText(legacyProductionPath, json, System.Text.Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, $"Sync appsettings to legacy path {legacyProductionPath} failed.");
            }
        }

        internal static void LogLoadedSettings(AppSettings settings, string modPath)
        {
            string legacyPath = LegacyProductionConfigPath();
            string modProductionPath = ModProductionConfigPath(modPath);
            var sources = new List<string> { "Config/appsettings.json" };
            if (File.Exists(legacyPath))
            {
                sources.Add("Managed/LSTY_Data/appsettings.json");
            }
            if (File.Exists(modProductionPath))
            {
                sources.Add("Mod/LSTY_Data/appsettings.json");
            }

            string keyStatus = string.IsNullOrWhiteSpace(settings.PanelApiKey)
                ? "NOT SET"
                : $"set ({settings.PanelApiKey.Length} chars)";

            CustomLogger.Info(
                $"App settings loaded ({string.Join(" -> ", sources)}): ApiOnly={settings.ApiOnly}, ServerId={settings.ServerId}, PanelApiKey={keyStatus}");
        }
    }
}
