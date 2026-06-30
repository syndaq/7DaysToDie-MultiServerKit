using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.FunctionSettings;

namespace SdtdMultiServerKit.Managers
{
    public sealed class PvpAreaZoneRules
    {
        public string KillMode { get; init; } = "strangers";

        public string DropOnDeath { get; init; } = "none";

        public int OnlineLandClaimBonus { get; init; }

        public int OfflineLandClaimBonus { get; init; }

        public string NoticeBuff { get; init; } = string.Empty;

        public bool InvulnerableClaim { get; init; }

        public bool IsCustomArea { get; init; }

        public string? AreaNote { get; init; }
    }

    public static class PvpAreaManager
    {
        private static readonly object SyncRoot = new();
        private static PvpAreaSettings _settings = new();
        private static List<T_PvpArea> _areas = new();

        public static void Reload(PvpAreaSettings settings, IEnumerable<T_PvpArea> areas)
        {
            lock (SyncRoot)
            {
                _settings = settings;
                _areas = areas.OrderBy(area => area.SortOrder).ThenBy(area => area.CreatedAt).ToList();
            }
        }

        public static bool IsEnabled
        {
            get
            {
                lock (SyncRoot)
                {
                    return _settings.IsEnabled;
                }
            }
        }

        public static PvpAreaZoneRules GetRulesAt(int x, int z)
        {
            lock (SyncRoot)
            {
                foreach (var area in _areas)
                {
                    if (IsInside(area, x, z))
                    {
                        return new PvpAreaZoneRules
                        {
                            KillMode = area.KillMode,
                            DropOnDeath = area.DropOnDeath,
                            OnlineLandClaimBonus = area.OnlineLandClaimBonus,
                            OfflineLandClaimBonus = area.OfflineLandClaimBonus,
                            NoticeBuff = area.AreaNoticeBuff,
                            InvulnerableClaim = area.InvulnerableClaim,
                            IsCustomArea = true,
                            AreaNote = area.AreaNote,
                        };
                    }
                }

                return new PvpAreaZoneRules
                {
                    KillMode = _settings.KillMode,
                    DropOnDeath = _settings.DropOnDeath,
                    OnlineLandClaimBonus = _settings.OnlineLandClaimBonus,
                    OfflineLandClaimBonus = _settings.OfflineLandClaimBonus,
                    NoticeBuff = _settings.DefaultNoticeBuff,
                    InvulnerableClaim = false,
                    IsCustomArea = false,
                };
            }
        }

        private static bool IsInside(T_PvpArea area, int x, int z)
        {
            int minX = Math.Min(area.X1, area.X2);
            int maxX = Math.Max(area.X1, area.X2);
            int minZ = Math.Min(area.Z1, area.Z2);
            int maxZ = Math.Max(area.Z1, area.Z2);
            return x >= minX && x <= maxX && z >= minZ && z <= maxZ;
        }
    }
}
