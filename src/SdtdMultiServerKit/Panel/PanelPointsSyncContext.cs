namespace SdtdMultiServerKit.Panel
{
    /// <summary>
    /// Prevents cluster point pushes when the panel is writing cache-only updates to this server.
    /// </summary>
    public static class PanelPointsSyncContext
    {
        [ThreadStatic]
        private static bool _suppressClusterPush;

        public static bool SuppressClusterPush
        {
            get => _suppressClusterPush;
            set => _suppressClusterPush = value;
        }

        public static IDisposable EnterSuppressScope()
        {
            return new SuppressScope();
        }

        private sealed class SuppressScope : IDisposable
        {
            private readonly bool _previous;

            public SuppressScope()
            {
                _previous = _suppressClusterPush;
                _suppressClusterPush = true;
            }

            public void Dispose()
            {
                _suppressClusterPush = _previous;
            }
        }
    }
}
