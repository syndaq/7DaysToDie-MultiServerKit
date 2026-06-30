using HarmonyLib;
using System.Reflection;

namespace SdtdMultiServerKit
{
    /// <summary>
    /// On .NET Framework, SQLitePCLRaw.bundle_e_sqlite3 uses the dynamic native loader which
    /// depends on System.Runtime.InteropServices.RuntimeInformation (missing on Linux 7DTD).
    /// Force the static e_sqlite3 provider instead.
    /// Native libraries live under Native/ — never in the mod root (7DTD auto-loads every *.dll there).
    /// </summary>
    internal static class SqliteBootstrap
    {
        private static bool _initialized;

        internal static void Initialize(string modPath)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            try
            {
                MethodInfo? initMethod = AccessTools.Method(typeof(SQLitePCL.Batteries_V2), "Init");
                if (initMethod != null)
                {
                    var harmony = new Harmony("sdtd.multiserverkit.sqlite");
                    harmony.Patch(
                        initMethod,
                        prefix: new HarmonyMethod(typeof(SqliteBootstrap), nameof(BatteriesInitPrefix)));
                }

                EnsureNativeLibrary(modPath);
            }
            catch (Exception ex)
            {
                LogBootstrap("SQLite bootstrap setup failed: {0}", ex.Message);
            }
        }

        internal static bool BatteriesInitPrefix()
        {
            try
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                SQLitePCL.raw.FreezeProvider();
                LogBootstrap("SQLite provider: e_sqlite3 (static)");
            }
            catch (Exception ex)
            {
                LogBootstrap("SQLite provider init failed: {0}", ex.Message);
            }

            return false;
        }

        internal static void EnsureNativeLibrary(string modPath)
        {
            if (PlatformHelper.IsLinux)
            {
                InstallNativeLibrary(
                    modPath,
                    new[] { "Native/linux-x64/libe_sqlite3.so", "libe_sqlite3.so" },
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "7DaysToDieServer_Data",
                        "MonoBleedingEdge",
                        "x86_64",
                        "libe_sqlite3.so"));
                return;
            }

            if (PlatformHelper.IsWindows)
            {
                InstallNativeLibrary(
                    modPath,
                    new[] { "Native/win-x64/e_sqlite3.dll" },
                    Path.Combine(AppContext.BaseDirectory, "e_sqlite3.dll"));
            }
        }

        private static void InstallNativeLibrary(string modPath, string[] relativeCandidates, string destPath)
        {
            string? srcPath = FindFirstExisting(modPath, relativeCandidates);
            if (srcPath == null)
            {
                LogBootstrap(
                    "Native SQLite library not found under {0}. Checked: {1}",
                    modPath,
                    string.Join(", ", relativeCandidates));
                return;
            }

            try
            {
                if (!File.Exists(destPath))
                {
                    string? destDir = Path.GetDirectoryName(destPath);
                    if (!string.IsNullOrEmpty(destDir))
                    {
                        Directory.CreateDirectory(destDir);
                    }

                    File.Copy(srcPath, destPath, overwrite: false);
                    LogBootstrap("Installed native SQLite library at {0}", destPath);
                }
            }
            catch (Exception ex)
            {
                LogBootstrap("Could not install native SQLite library: {0}", ex.Message);
            }
        }

        private static string? FindFirstExisting(string modPath, IEnumerable<string> relativeCandidates)
        {
            foreach (string relative in relativeCandidates)
            {
                string path = Path.Combine(modPath, relative);
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return null;
        }

        private static void LogBootstrap(string message, params object[] args)
        {
            try
            {
                message = CustomLogger.Prefix + message;
                if (args.Length > 0)
                {
                    Log.Out(message, args);
                }
                else
                {
                    Log.Out(message);
                }
            }
            catch
            {
                // Log may not be ready during very early assembly load.
            }
        }
    }
}
