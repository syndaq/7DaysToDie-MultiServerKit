using System.Reflection;

namespace SdtdMultiServerKit
{
    /// <summary>
    /// Resolves Mono framework assemblies from the game's MonoBleedingEdge tree.
    /// Do not place these DLLs in the mod root — 7DTD auto-loads every *.dll there and
    /// Ubuntu/debian mono-devel copies fail with Invalid Image under Unity MonoBleedingEdge.
    /// </summary>
    internal static class MonoFrameworkAssemblyLoader
    {
        private static readonly object Sync = new();
        private static bool _registered;
        private static readonly List<string> SearchDirectories = new();
        private static readonly HashSet<string> FrameworkAssemblyNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "System.ComponentModel.DataAnnotations",
            "System.Reflection.Emit",
            "System.Reflection.Emit.ILGeneration",
            "System.Reflection.Emit.Lightweight",
        };

        internal static void Register(string modPath)
        {
            lock (Sync)
            {
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

            string serverRoot = Path.GetFullPath(Path.Combine(modPath, "..", ".."));
            string dataDir = Path.Combine(serverRoot, "7DaysToDieServer_Data");
            string monoRoot = Path.Combine(dataDir, "MonoBleedingEdge", "lib", "mono");

            foreach (string sub in new[] { "4.5-api", "4.5", "4.0-api", "4.0" })
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

            string managed = Path.Combine(dataDir, "Managed");
            if (Directory.Exists(managed))
            {
                yield return managed;
            }
        }

        private static Assembly? OnAssemblyResolve(object? sender, ResolveEventArgs args)
        {
            string? name = new AssemblyName(args.Name).Name;
            if (name == null || !FrameworkAssemblyNames.Contains(name))
            {
                return null;
            }

            return TryLoadAssembly(name);
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
                        CustomLogger.Info("Preloaded framework assembly {0} from {1}", name, asm.Location);
                    }
                    else
                    {
                        CustomLogger.Warn("Framework assembly not found on disk: {0}", name);
                    }
                }
                catch (Exception ex)
                {
                    CustomLogger.Warn("Could not preload {0}: {1}", name, ex.Message);
                }
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
                    CustomLogger.Warn("Failed to load {0}: {1}", path, ex.Message);
                }
            }

            return null;
        }
    }
}
