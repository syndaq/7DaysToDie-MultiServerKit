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

        internal static string ModDataDirectory(string modPath) =>
            Path.Combine(modPath, LstyDataFolder);

        /// <summary>
        /// Resolve SQLite database path. Relative paths use the mod's LSTY_Data folder (writable on
        /// hosted servers). Falls back to a legacy AppContext.BaseDirectory path when an existing DB
        /// is found there.
        /// </summary>
        internal static string ResolveDatabasePath(string modPath, string configuredPath)
        {
            if (string.IsNullOrWhiteSpace(configuredPath))
            {
                configuredPath = "database.db";
            }

            if (Path.IsPathRooted(configuredPath))
            {
                return Path.GetFullPath(configuredPath);
            }

            string relative = configuredPath.Replace('\\', '/');
            const string legacyPrefix = LstyDataFolder + "/";
            if (relative.StartsWith(legacyPrefix, StringComparison.OrdinalIgnoreCase))
            {
                relative = relative.Substring(legacyPrefix.Length);
            }

            if (string.IsNullOrWhiteSpace(relative))
            {
                relative = "database.db";
            }

            string modDbPath = Path.GetFullPath(Path.Combine(ModDataDirectory(modPath), relative));
            string legacyDbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, configuredPath));

            if (!File.Exists(modDbPath) && File.Exists(legacyDbPath))
            {
                return legacyDbPath;
            }

            return modDbPath;
        }

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

        private static string FormatKeyPrefix(string key)
        {
            if (key.Length <= 12)
            {
                return key;
            }

            return key.Substring(0, 12) + "…";
        }

        internal static void LogLoadedSettings(AppSettings settings, string modPath)
        {
            string defaultPath = DefaultConfigPath(modPath);
            string legacyPath = LegacyProductionConfigPath();
            string modProductionPath = ModProductionConfigPath(modPath);
            var sources = new List<string> { defaultPath };
            if (File.Exists(legacyPath))
            {
                sources.Add(legacyPath);
            }
            if (File.Exists(modProductionPath))
            {
                sources.Add(modProductionPath);
            }

            string keyStatus = string.IsNullOrWhiteSpace(settings.PanelApiKey)
                ? "NOT SET"
                : $"prefix={FormatKeyPrefix(settings.PanelApiKey)} ({settings.PanelApiKey.Length} chars)";

            CustomLogger.Info(
                $"App settings loaded ({string.Join(" -> ", sources)}): ApiOnly={settings.ApiOnly}, ServerId={settings.ServerId}, WebUrl={settings.WebUrl}, PanelApiKey={keyStatus}");

            if (File.Exists(modProductionPath) || File.Exists(legacyPath))
            {
                CustomLogger.Info(
                    "Runtime config overrides are active — later paths win. Edit or delete the override file(s) above; Config/appsettings.json alone is not enough after first run.");
            }
        }
    }
}
