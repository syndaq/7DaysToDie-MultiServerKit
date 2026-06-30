using System.Reflection;
using System.Runtime.CompilerServices;

namespace SdtdMultiServerKit
{
    /// <summary>
    /// Resolves Mono framework assemblies from the game's Managed folder and mod Framework/.
    /// Do not place dependency DLLs in the mod root — 7DTD auto-loads every *.dll there.
    /// Release packages put NuGet dependencies under Lib/ and resolve them at runtime.
    /// Ubuntu/debian mono-devel copies are not the same as the game's Managed assemblies.
    /// </summary>
    internal static class MonoFrameworkAssemblyLoader
    {
        private static readonly object Sync = new();
        private static bool _registered;
        private static string? _modRootPath;
        private static readonly List<string> SearchDirectories = new();
        private static readonly HashSet<string> FrameworkAssemblyNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "System.ComponentModel.DataAnnotations",
            "System.Reflection.Emit",
            "System.Reflection.Emit.ILGeneration",
            "System.Reflection.Emit.Lightweight",
        };

        private static readonly HashSet<string> PreloadLogIfMissing = new(StringComparer.OrdinalIgnoreCase)
        {
            "System.ComponentModel.DataAnnotations",
        };

        [ModuleInitializer]
        internal static void Initialize()
        {
            string? modDll = Assembly.GetExecutingAssembly().Location;
            if (string.IsNullOrEmpty(modDll))
            {
                return;
            }

            string? modPath = Path.GetDirectoryName(modDll);
            if (string.IsNullOrEmpty(modPath))
            {
                return;
            }

            Register(modPath);
            SqliteBootstrap.Initialize(modPath);
        }

        internal static void Register(string modPath)
        {
            lock (Sync)
            {
                _modRootPath = modPath;
                RefreshSearchDirectories(modPath);

                if (_registered)
                {
                    return;
                }

                _registered = true;
                AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
                PreloadFrameworkAssemblies();
            }
        }

        private static void RefreshSearchDirectories(string modPath)
        {
            SearchDirectories.Clear();

            string libDir = Path.Combine(modPath, "Lib");
            if (Directory.Exists(libDir))
            {
                SearchDirectories.Add(libDir);
            }

            foreach (string dir in GetMonoSearchDirectories(modPath))
            {
                if (!SearchDirectories.Contains(dir))
                {
                    SearchDirectories.Add(dir);
                }
            }
        }

        internal static IEnumerable<string> GetMonoSearchDirectories(string modPath)
        {
            string framework = Path.Combine(modPath, "Framework");
            if (Directory.Exists(framework))
            {
                yield return framework;
            }

            foreach (string dataDir in GetGameDataDirectories(modPath))
            {
                string managed = Path.Combine(dataDir, "Managed");
                if (Directory.Exists(managed))
                {
                    yield return managed;
                }

                string monoRoot = Path.Combine(dataDir, "MonoBleedingEdge", "lib", "mono");
                if (!Directory.Exists(monoRoot))
                {
                    continue;
                }

                foreach (string sub in new[]
                {
                    "unityjit-linux-x86_64",
                    "unityjit",
                    "unityaot-linux-x86_64",
                    "unityaot",
                    "4.5-api",
                    "4.5",
                    "4.0-api",
                    "4.0",
                })
                {
                    string dir = Path.Combine(monoRoot, sub);
                    if (!Directory.Exists(dir))
                    {
                        continue;
                    }

                    yield return dir;

                    string facades = Path.Combine(dir, "Facades");
                    if (Directory.Exists(facades))
                    {
                        yield return facades;
                    }
                }
            }
        }

        private static IEnumerable<string> GetGameDataDirectories(string modPath)
        {
            var candidates = new[]
            {
                Path.Combine(modPath, "..", "..", "7DaysToDieServer_Data"),
                Path.Combine(modPath, "..", "..", "..", "7DaysToDieServer_Data"),
                Path.Combine(AppContext.BaseDirectory, "7DaysToDieServer_Data"),
            };

            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (string candidate in candidates)
            {
                string fullPath = Path.GetFullPath(candidate);
                if (seen.Add(fullPath) && Directory.Exists(fullPath))
                {
                    yield return fullPath;
                }
            }
        }

        private static Assembly? OnAssemblyResolve(object? sender, ResolveEventArgs args)
        {
            string? name = new AssemblyName(args.Name).Name;
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (FrameworkAssemblyNames.Contains(name) || IsBundledFrameworkAssembly(name) || HasLibAssembly(name))
            {
                return TryLoadAssembly(name);
            }

            return null;
        }

        private static bool HasLibAssembly(string assemblyName)
        {
            if (string.IsNullOrEmpty(_modRootPath))
            {
                return false;
            }

            string libPath = Path.Combine(_modRootPath, "Lib", assemblyName + ".dll");
            return File.Exists(libPath);
        }

        private static bool IsBundledFrameworkAssembly(string assemblyName)
        {
            string? frameworkDir = GetFrameworkDirectory();
            return frameworkDir != null
                && File.Exists(Path.Combine(frameworkDir, assemblyName + ".dll"));
        }

        private static string? GetFrameworkDirectory()
        {
            foreach (string dir in SearchDirectories)
            {
                if (string.Equals(Path.GetFileName(dir), "Framework", StringComparison.OrdinalIgnoreCase))
                {
                    return dir;
                }
            }

            return null;
        }

        private static void PreloadFrameworkAssemblies()
        {
            foreach (string name in FrameworkAssemblyNames)
            {
                try
                {
                    if (IsAssemblyLoaded(name))
                    {
                        continue;
                    }

                    Assembly? asm = TryLoadAssembly(name);
                    if (asm != null)
                    {
                        LogBootstrap("Preloaded framework assembly {0} from {1}", name, asm.Location);
                    }
                    else if (PreloadLogIfMissing.Contains(name))
                    {
                        LogBootstrap(
                            "Framework assembly not found on disk: {0}. Searched: {1}",
                            name,
                            string.Join(", ", SearchDirectories));
                    }
                }
                catch (Exception ex)
                {
                    LogBootstrap("Could not preload {0}: {1}", name, ex.Message);
                }
            }
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

        private static bool IsAssemblyLoaded(string assemblyName)
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (string.Equals(asm.GetName().Name, assemblyName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static Assembly? TryLoadAssembly(string assemblyName)
        {
            foreach (string dir in SearchDirectories)
            {
                string path = Path.Combine(dir, assemblyName + ".dll");
                if (!File.Exists(path))
                {
                    continue;
                }

                try
                {
                    return Assembly.LoadFrom(path);
                }
                catch (Exception ex)
                {
                    LogBootstrap("Failed to load {0}: {1}", path, ex.Message);
                }
            }

            return null;
        }
    }
}
