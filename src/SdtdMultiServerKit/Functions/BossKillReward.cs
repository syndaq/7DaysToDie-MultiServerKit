using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;
using SdtdMultiServerKit.Variables;

namespace SdtdMultiServerKit.Functions
{
    public class BossKillReward : FunctionBase<BossKillRewardSettings>
    {
        public BossKillReward()
        {
            ModEventHub.EntityKilled += OnEntityKilled;
        }

        private void OnEntityKilled(KilledEntity killedEntity)
        {
            if (!Settings.IsEnabled)
            {
                return;
            }

            try
            {
                if (killedEntity.DeadEntity.EntityType is not (Models.EntityType.Zombie or Models.EntityType.Animal))
                {
                    return;
                }

                if (!LivePlayerManager.TryGetByEntityId(killedEntity.KillerEntityId, out var killer) || killer == null)
                {
                    return;
                }

                string entityName = killedEntity.DeadEntity.EntityName;
                int reward = Settings.FallbackReward;
                if (Settings.EnemyRewardMap != null
                    && Settings.EnemyRewardMap.TryGetValue(entityName, out int mappedReward))
                {
                    reward = mappedReward;
                }

                if (reward == 0)
                {
                    return;
                }

                Utilities.Utils.ExecuteConsoleCommand($"ty-cpp {killer.EntityId} {reward}", true);

                if (string.IsNullOrEmpty(Settings.KillTip) == false)
                {
                    string tip = StringTemplate.Render(Settings.KillTip, new VariablesBase
                    {
                        EntityId = killer.EntityId,
                        PlayerId = killer.PlayerId,
                        PlayerName = killer.PlayerName,
                    }).Replace("{EntityName}", entityName);

                    SendMessageToPlayer(killer.PlayerId, tip);
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, "Error in BossKillReward.OnEntityKilled");
            }
        }
    }
}
