namespace SdtdMultiServerKit
{
    internal static class PlatformHelper
    {
        internal static bool IsLinux
        {
            get
            {
                PlatformID platform = Environment.OSVersion.Platform;
                return platform == PlatformID.Unix || platform == PlatformID.MacOSX;
            }
        }

        internal static bool IsWindows => Environment.OSVersion.Platform == PlatformID.Win32NT;
    }
}
