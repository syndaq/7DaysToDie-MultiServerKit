//using HarmonyLib;
//using UnityEngine;

//namespace SdtdMultiServerKit.HarmonyPatchers.PerformanceTuning
//{
//    [HarmonyPatch]
//    internal class AstarManagerPatcher
//    {
//        [HarmonyPrefix]
//        [HarmonyPatch(typeof(AstarManager), nameof(AstarManager.Init))]
//        public static bool Init(GameObject obj)
//        {
//            if (GamePrefs.GetString(EnumGamePrefs.GameWorld) == "Empty")
//            {
//                return false;
//            }

//            CustomLogger.Info("AstarManager Init");
//            obj.AddComponent<AstarManager>();
//            new SdtdMultiServerKit.HarmonyPatchers.PerformanceTuning.ASPPathFinderThread().StartWorkerThreads();
//            return false;
//        }
//    }
//}