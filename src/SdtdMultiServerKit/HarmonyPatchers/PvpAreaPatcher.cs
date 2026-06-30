using HarmonyLib;
using SdtdMultiServerKit.Compat;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.HarmonyPatchers
{
    [HarmonyPatch(typeof(EntityAlive))]
    internal static class PvpAreaPatcher
    {
        public static bool Before_dropItemOnDeath(EntityAlive __instance)
        {
            try
            {
                if (!PvpAreaManager.IsEnabled || __instance is not EntityPlayer player)
                {
                    return true;
                }

                var position = player.GetBlockPosition();
                string dropMode = PvpAreaManager.GetRulesAt(position.x, position.z).DropOnDeath;
                PvpAreaDropHelper.ApplyDropMode(player, dropMode);
                return PvpAreaDropHelper.ShouldRunVanillaDrop(dropMode);
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, "Error in PvpAreaPatcher.Before_dropItemOnDeath");
                return true;
            }
        }

        public static void Prefix_IsLandProtectedBlock(Vector3i blockPos, ref int claimSize, PersistentPlayerData lpRelative)
        {
            try
            {
                if (!PvpAreaManager.IsEnabled)
                {
                    return;
                }

                var rules = PvpAreaManager.GetRulesAt(blockPos.x, blockPos.z);
                int bonus = rules.OfflineLandClaimBonus;
                if (lpRelative != null && LivePlayerManager.TryGetByPlayerId(lpRelative.PrimaryId.CombinedString, out _))
                {
                    bonus = rules.OnlineLandClaimBonus;
                }

                if (bonus > 0)
                {
                    claimSize += bonus;
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, "Error in PvpAreaPatcher.Prefix_IsLandProtectedBlock");
            }
        }

        public static bool Before_DamageEntity(
            EntityAlive __instance,
            DamageSource _damageSource,
            int _strength,
            bool _criticalHit,
            float _impulseScale)
        {
            try
            {
                if (!PvpAreaManager.IsEnabled || _strength <= 0 || __instance is not EntityPlayer victim)
                {
                    return true;
                }

                int attackerEntityId = _damageSource.getEntityId();
                if (attackerEntityId <= 0)
                {
                    return true;
                }

                var attacker = __instance.world?.GetEntity(attackerEntityId) as EntityPlayer;
                if (attacker == null)
                {
                    return true;
                }

                if (PvpAreaRules.CanPlayersDamageEachOther(attacker, victim))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, "Error in PvpAreaPatcher.Before_DamageEntity");
                return true;
            }
        }

        public static void After_Explosion_AttackBlocks(Explosion __instance)
        {
            try
            {
                if (!PvpAreaManager.IsEnabled || __instance?.ChangedBlockPositions == null)
                {
                    return;
                }

                List<BlockChangeInfo>? restored = null;
                foreach (var pos in __instance.ChangedBlockPositions.Keys.ToList())
                {
                    if (!PvpAreaRules.IsLandClaimInvulnerableAt(pos.x, pos.z))
                    {
                        continue;
                    }

                    var block = GameManager.Instance.World.GetBlock(pos).Block;
                    if (block is not BlockLandClaim)
                    {
                        continue;
                    }

                    restored ??= new List<BlockChangeInfo>();
                    restored.Add(GameCompatBridge.CreateRestoreBlockChange(pos, GameManager.Instance.World.GetBlock(pos), __instance));
                    __instance.ChangedBlockPositions.Remove(pos);
                }

                if (restored != null && restored.Count > 0)
                {
                    var package = NetPackageManager.GetPackage<NetPackageSetBlock>().Setup(null, restored, -1);
                    ConnectionManager.Instance.Clients.ForEntityId(__instance.entityId)?.SendPackage(package);
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, "Error in PvpAreaPatcher.After_Explosion_AttackBlocks");
            }
        }
    }
}
