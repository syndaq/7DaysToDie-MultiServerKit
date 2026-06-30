using SdtdMultiServerKit.Models;
using SdtdMultiServerKit.Panel;

namespace SdtdMultiServerKit.Managers
{
    public static class ClusterPointsLoginSync
    {
        public static void Register()
        {
            ModEventHub.PlayerLogin += OnPlayerLogin;
        }

        private static async void OnPlayerLogin(PlayerBase playerBase)
        {
            try
            {
                var panel = ModApi.ServiceContainer.Resolve<PanelPointsClient>();
                if (!panel.IsEnabled)
                {
                    return;
                }

                var record = await panel.GetPointsAsync(playerBase.PlayerId).ConfigureAwait(false);
                if (record == null)
                {
                    return;
                }

                var repository = ModApi.ServiceContainer.Resolve<Data.IRepositories.IPointsInfoRepository>();
                if (repository is Data.Repositories.PointsInfoRepository pointsRepository)
                {
                    using (PanelPointsSyncContext.EnterSuppressScope())
                    {
                        await pointsRepository.SetLocalPointsAsync(playerBase.PlayerId, record).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Warn(ex, "Failed to sync cluster points on login for {PlayerId}", playerBase.PlayerId);
            }
        }
    }
}
