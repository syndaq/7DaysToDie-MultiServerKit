namespace SdtdMultiServerKit.Managers
{
    internal static class PvpAreaPlayerTracker
    {
        private static readonly object SyncRoot = new();
        private static readonly Dictionary<int, string> CurrentBuffByEntity = new();
        private static readonly Dictionary<int, string> CurrentZoneKeyByEntity = new();

        public static void UpdateOnlinePlayers()
        {
            if (!PvpAreaManager.IsEnabled)
            {
                return;
            }

            var players = GameManager.Instance?.World?.Players?.dict;
            if (players == null)
            {
                return;
            }

            foreach (var player in players.Values)
            {
                try
                {
                    UpdatePlayer(player);
                }
                catch (Exception ex)
                {
                    CustomLogger.Warn(ex, "Error updating PVP area state for entity {0}", player.entityId);
                }
            }
        }

        public static void ClearPlayer(int entityId)
        {
            lock (SyncRoot)
            {
                CurrentBuffByEntity.Remove(entityId);
                CurrentZoneKeyByEntity.Remove(entityId);
            }
        }

        public static void ClearAll()
        {
            lock (SyncRoot)
            {
                CurrentBuffByEntity.Clear();
                CurrentZoneKeyByEntity.Clear();
            }
        }

        private static void UpdatePlayer(EntityPlayer player)
        {
            var blockPos = player.GetBlockPosition();
            var rules = PvpAreaManager.GetRulesAt(blockPos.x, blockPos.z);
            string zoneKey = rules.IsCustomArea
                ? $"area:{rules.AreaNote}:{blockPos.x}:{blockPos.z}"
                : "default";
            string buffName = rules.NoticeBuff ?? string.Empty;

            lock (SyncRoot)
            {
                if (CurrentZoneKeyByEntity.TryGetValue(player.entityId, out var existingZone)
                    && existingZone == zoneKey)
                {
                    return;
                }

                if (CurrentBuffByEntity.TryGetValue(player.entityId, out var previousBuff)
                    && string.IsNullOrEmpty(previousBuff) == false)
                {
                    player.Buffs.RemoveBuff(previousBuff);
                }

                if (string.IsNullOrEmpty(buffName) == false)
                {
                    player.Buffs.AddBuff(buffName);
                }

                CurrentBuffByEntity[player.entityId] = buffName;
                CurrentZoneKeyByEntity[player.entityId] = zoneKey;
            }
        }
    }
}
