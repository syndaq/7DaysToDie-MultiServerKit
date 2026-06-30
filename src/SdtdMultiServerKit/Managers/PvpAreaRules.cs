using SdtdMultiServerKit.Utilities;

namespace SdtdMultiServerKit.Managers
{
    internal static class PvpAreaRules
    {
        public static bool CanPlayersDamageEachOther(EntityPlayer attacker, EntityPlayer victim)
        {
            if (!PvpAreaManager.IsEnabled || attacker == null || victim == null)
            {
                return true;
            }

            if (attacker.entityId == victim.entityId)
            {
                return true;
            }

            var attackerPos = attacker.GetBlockPosition();
            var victimPos = victim.GetBlockPosition();
            var attackerRules = PvpAreaManager.GetRulesAt(attackerPos.x, attackerPos.z);
            var victimRules = PvpAreaManager.GetRulesAt(victimPos.x, victimPos.z);

            return IsAttackAllowed(attackerRules.KillMode, attacker.entityId, victim.entityId)
                && IsVictimDamageable(victimRules.KillMode, attacker.entityId, victim.entityId);
        }

        private static bool IsAttackAllowed(string killMode, int attackerEntityId, int victimEntityId)
        {
            return killMode switch
            {
                "none" => false,
                "allies" => Utilities.Utils.IsFriend(attackerEntityId, victimEntityId),
                "strangers" => !Utilities.Utils.IsFriend(attackerEntityId, victimEntityId),
                "everyone" => true,
                _ => true,
            };
        }

        private static bool IsVictimDamageable(string killMode, int attackerEntityId, int victimEntityId)
        {
            return killMode switch
            {
                "none" => false,
                "allies" => Utilities.Utils.IsFriend(attackerEntityId, victimEntityId),
                "strangers" => !Utilities.Utils.IsFriend(attackerEntityId, victimEntityId),
                "everyone" => true,
                _ => true,
            };
        }

        public static bool IsLandClaimInvulnerableAt(int x, int z)
        {
            if (!PvpAreaManager.IsEnabled)
            {
                return false;
            }

            return PvpAreaManager.GetRulesAt(x, z).InvulnerableClaim;
        }
    }
}
