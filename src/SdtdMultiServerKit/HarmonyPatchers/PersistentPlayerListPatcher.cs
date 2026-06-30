using HarmonyLib;

namespace SdtdMultiServerKit.HarmonyPatchers
{
    [HarmonyPatch(typeof(PersistentPlayerList))]
    internal class PersistentPlayerListPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PersistentPlayerList.CleanupPlayers))]
        public static bool Before_CleanupPlayers(ref bool __result)
        {
            __result = false;
            return false;
        }
    }
}