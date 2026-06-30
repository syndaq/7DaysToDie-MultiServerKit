using HarmonyLib;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.HarmonyPatchers;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.Functions
{
    public class PvpArea : FunctionBase<PvpAreaSettings>
    {
        private readonly IPvpAreaRepository _pvpAreaRepository;
        private readonly SubTimer _zoneTimer;
        private bool _damagePatchApplied;
        private bool _explosionPatchApplied;
        private bool _dropPatchApplied;
        private bool _landClaimPatchApplied;

        public PvpArea(IPvpAreaRepository pvpAreaRepository)
        {
            _pvpAreaRepository = pvpAreaRepository;
            _zoneTimer = new SubTimer(UpdateZones) { Interval = 2 };
        }

        protected override void OnSettingsChanged()
        {
            ReloadAreas(Settings);
            ApplyHarmonyPatches();
            _zoneTimer.IsEnabled = Settings.IsEnabled;
        }

        protected override void OnEnableFunction()
        {
            base.OnEnableFunction();
            GlobalTimer.RegisterSubTimer(_zoneTimer);
            ModEventHub.PlayerDisconnected += OnPlayerDisconnected;
            ApplyHarmonyPatches();
            ReloadAreas(Settings);
        }

        protected override void OnDisableFunction()
        {
            GlobalTimer.UnregisterSubTimer(_zoneTimer);
            ModEventHub.PlayerDisconnected -= OnPlayerDisconnected;
            RemoveHarmonyPatches();
            PvpAreaPlayerTracker.ClearAll();
            base.OnDisableFunction();
        }

        private void OnPlayerDisconnected(ManagedPlayer managedPlayer)
        {
            PvpAreaPlayerTracker.ClearPlayer(managedPlayer.EntityId);
        }

        private void UpdateZones()
        {
            PvpAreaPlayerTracker.UpdateOnlinePlayers();
        }

        private void ReloadAreas(PvpAreaSettings settings)
        {
            var areas = _pvpAreaRepository.GetAllAsync().GetAwaiter().GetResult();
            PvpAreaManager.Reload(settings, areas);
            PvpAreaPlayerTracker.ClearAll();
        }

        private void ApplyHarmonyPatches()
        {
            if (Settings.IsEnabled)
            {
                if (!_damagePatchApplied)
                {
                    var damageMethod = AccessTools.Method(typeof(EntityAlive), nameof(EntityAlive.DamageEntity));
                    var damagePatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.Before_DamageEntity));
                    if (damageMethod != null && damagePatch != null)
                    {
                        ModApi.Harmony.Patch(damageMethod, prefix: new HarmonyMethod(damagePatch));
                        _damagePatchApplied = true;
                    }
                    else
                    {
                        CustomLogger.Warn("PVP area damage patch could not be applied.");
                    }
                }

                if (!_explosionPatchApplied)
                {
                    var explosionMethod = AccessTools.Method(typeof(Explosion), nameof(Explosion.AttackBlocks));
                    var explosionPatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.After_Explosion_AttackBlocks));
                    if (explosionMethod != null && explosionPatch != null)
                    {
                        ModApi.Harmony.Patch(explosionMethod, postfix: new HarmonyMethod(explosionPatch));
                        _explosionPatchApplied = true;
                    }
                }

                if (!_dropPatchApplied)
                {
                    var dropMethod = AccessTools.Method(typeof(EntityAlive), "dropItemOnDeath");
                    var dropPatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.Before_dropItemOnDeath));
                    if (dropMethod != null && dropPatch != null)
                    {
                        ModApi.Harmony.Patch(dropMethod, prefix: new HarmonyMethod(dropPatch));
                        _dropPatchApplied = true;
                    }
                    else
                    {
                        CustomLogger.Warn("PVP area drop-on-death patch could not be applied.");
                    }
                }

                if (!_landClaimPatchApplied)
                {
                    var landClaimMethod = AccessTools.Method(typeof(GameManager), "IsLandProtectedBlock");
                    var landClaimPatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.Prefix_IsLandProtectedBlock));
                    if (landClaimMethod != null && landClaimPatch != null)
                    {
                        ModApi.Harmony.Patch(landClaimMethod, prefix: new HarmonyMethod(landClaimPatch));
                        _landClaimPatchApplied = true;
                    }
                    else
                    {
                        CustomLogger.Warn("PVP area land claim bonus patch could not be applied.");
                    }
                }
            }
            else
            {
                RemoveHarmonyPatches();
            }
        }

        private void RemoveHarmonyPatches()
        {
            if (_damagePatchApplied)
            {
                var damageMethod = AccessTools.Method(typeof(EntityAlive), nameof(EntityAlive.DamageEntity));
                var damagePatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.Before_DamageEntity));
                if (damageMethod != null && damagePatch != null)
                {
                    ModApi.Harmony.Unpatch(damageMethod, damagePatch);
                }
                _damagePatchApplied = false;
            }

            if (_explosionPatchApplied)
            {
                var explosionMethod = AccessTools.Method(typeof(Explosion), nameof(Explosion.AttackBlocks));
                var explosionPatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.After_Explosion_AttackBlocks));
                if (explosionMethod != null && explosionPatch != null)
                {
                    ModApi.Harmony.Unpatch(explosionMethod, explosionPatch);
                }
                _explosionPatchApplied = false;
            }

            if (_dropPatchApplied)
            {
                var dropMethod = AccessTools.Method(typeof(EntityAlive), "dropItemOnDeath");
                var dropPatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.Before_dropItemOnDeath));
                if (dropMethod != null && dropPatch != null)
                {
                    ModApi.Harmony.Unpatch(dropMethod, dropPatch);
                }
                _dropPatchApplied = false;
            }

            if (_landClaimPatchApplied)
            {
                var landClaimMethod = AccessTools.Method(typeof(GameManager), "IsLandProtectedBlock");
                var landClaimPatch = AccessTools.Method(typeof(PvpAreaPatcher), nameof(PvpAreaPatcher.Prefix_IsLandProtectedBlock));
                if (landClaimMethod != null && landClaimPatch != null)
                {
                    ModApi.Harmony.Unpatch(landClaimMethod, landClaimPatch);
                }
                _landClaimPatchApplied = false;
            }
        }
    }
}
