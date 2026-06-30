using System.Reflection;

namespace SdtdMultiServerKit.MapRendering
{
    /// <summary>
    /// Version-agnostic access to TFP map rendering APIs.
    /// v2.6: types live in Mods/TFP_MapRendering/MapRendering.dll
    /// v3.0+: types were merged into Assembly-CSharp (TFP_MapRendering / TFP_WebServer mods removed)
    /// </summary>
    internal static class MapRenderingBridge
    {
        private static Assembly? _assembly;
        private static Type? _constantsType;
        private static Type? _mapRendererType;
        private static Type? _mapTileCacheType;
        private static PropertyInfo? _mapRendererInstanceProperty;
        private static MethodInfo? _getTileCacheMethod;
        private static MethodInfo? _getFileContentMethod;
        private static MethodInfo? _renderFullMapMethod;

        public static bool IsAvailable { get; private set; }

        public static void Initialize()
        {
            _assembly = FindMapRenderingAssembly();
            if (_assembly == null)
            {
                IsAvailable = false;
                return;
            }

            _constantsType = _assembly.GetType("MapRendering.Constants", throwOnError: false);
            _mapRendererType = _assembly.GetType("MapRendering.MapRenderer", throwOnError: false);
            _mapTileCacheType = _assembly.GetType("MapRendering.MapTileCache", throwOnError: false);

            if (_mapRendererType != null)
            {
                _mapRendererInstanceProperty = _mapRendererType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
                _getTileCacheMethod = _mapRendererType.GetMethod("GetTileCache", BindingFlags.Public | BindingFlags.Static);
                _renderFullMapMethod = _mapRendererType.GetMethod("RenderFullMap", BindingFlags.Public | BindingFlags.Instance);
            }

            if (_mapTileCacheType != null)
            {
                _getFileContentMethod = _mapTileCacheType.GetMethod(
                    "GetFileContent",
                    BindingFlags.Public | BindingFlags.Instance,
                    binder: null,
                    types: new[] { typeof(string) },
                    modifiers: null);
            }

            IsAvailable = _constantsType != null && _mapRendererType != null && _getTileCacheMethod != null;
        }

        public static int MapBlockSize => ReadStaticInt(_constantsType, "MapBlockSize", 512);

        public static int ZoomLevels => ReadStaticInt(_constantsType, "Zoomlevels", 4);

        public static string MapDirectory => ReadStaticString(_constantsType, "MapDirectory", string.Empty);

        public static object? GetTileCache()
        {
            if (_getTileCacheMethod == null)
            {
                return null;
            }

            return _getTileCacheMethod.Invoke(null, null);
        }

        public static byte[]? GetFileContent(object tileCache, string fileName)
        {
            if (_getFileContentMethod == null)
            {
                return null;
            }

            return _getFileContentMethod.Invoke(tileCache, new object[] { fileName }) as byte[];
        }

        public static void RenderFullMap()
        {
            if (_mapRendererInstanceProperty == null || _renderFullMapMethod == null)
            {
                throw new InvalidOperationException("Map rendering is not available on this game version.");
            }

            var instance = _mapRendererInstanceProperty.GetValue(null)
                ?? throw new InvalidOperationException("MapRenderer.Instance is null.");

            _renderFullMapMethod.Invoke(instance, null);
        }

        private static Assembly? FindMapRenderingAssembly()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetType("MapRendering.MapRenderer", throwOnError: false) != null)
                {
                    return assembly;
                }
            }

            try
            {
                string modsRoot = Path.Combine(AppContext.BaseDirectory, "Mods");
                if (Directory.Exists(modsRoot))
                {
                    string[] files = Directory.GetFiles(modsRoot, "MapRendering.dll", SearchOption.AllDirectories);
                    if (files.Length > 0)
                    {
                        return Assembly.LoadFrom(files[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Warn($"Could not load MapRendering.dll from Mods: {ex.Message}");
            }

            return null;
        }

        private static int ReadStaticInt(Type? type, string name, int fallback)
        {
            if (type == null)
            {
                return fallback;
            }

            var field = type.GetField(name, BindingFlags.Public | BindingFlags.Static);
            if (field?.GetValue(null) is int value)
            {
                return value;
            }

            var property = type.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            if (property?.GetValue(null) is int propValue)
            {
                return propValue;
            }

            return fallback;
        }

        private static string ReadStaticString(Type? type, string name, string fallback)
        {
            if (type == null)
            {
                return fallback;
            }

            var field = type.GetField(name, BindingFlags.Public | BindingFlags.Static);
            if (field?.GetValue(null) is string value)
            {
                return value;
            }

            var property = type.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            if (property?.GetValue(null) is string propValue)
            {
                return propValue;
            }

            return fallback;
        }
    }
}
