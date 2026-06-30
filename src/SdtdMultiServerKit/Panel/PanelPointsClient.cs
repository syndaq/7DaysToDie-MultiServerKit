using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SdtdMultiServerKit.Panel
{
    public class PanelPointsClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public bool IsEnabled =>
            !string.IsNullOrWhiteSpace(ModApi.AppSettings.PanelUrl)
            && !string.IsNullOrWhiteSpace(ModApi.AppSettings.PanelApiKey)
            && !string.IsNullOrWhiteSpace(ModApi.AppSettings.ServerId);

        public async Task<PanelPointsRecord?> GetPointsAsync(string platformId)
        {
            if (!IsEnabled)
            {
                return null;
            }

            string url = $"{NormalizeBaseUrl(ModApi.AppSettings.PanelUrl)}/api/points/by-platform/{Uri.EscapeDataString(platformId)}";
            using var request = CreateRequest(HttpMethod.Get, url);
            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PanelPointsRecord>(json);
        }

        public async Task<PanelPointsRecord?> AdjustPointsAsync(
            string platformId,
            string displayName,
            int change,
            string category,
            string type,
            string? note = null,
            DateTime? lastSignInAt = null)
        {
            if (!IsEnabled || change == 0)
            {
                return null;
            }

            string url = $"{NormalizeBaseUrl(ModApi.AppSettings.PanelUrl)}/api/points/ingest";
            var body = new
            {
                platformId,
                displayName,
                change,
                category,
                type,
                note,
                lastSignInAt,
            };

            using var request = CreateRequest(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                CustomLogger.Warn("Panel points ingest failed ({StatusCode}) for player {PlayerId}", (int)response.StatusCode, platformId);
                return null;
            }

            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PanelPointsRecord>(json);
        }

        public async Task<PanelPointsRecord?> SetAbsolutePointsAsync(
            string platformId,
            string displayName,
            int points,
            DateTime? lastSignInAt = null)
        {
            if (!IsEnabled)
            {
                return null;
            }

            string url = $"{NormalizeBaseUrl(ModApi.AppSettings.PanelUrl)}/api/points/ingest";
            var body = new
            {
                platformId,
                displayName,
                absolutePoints = points,
                lastSignInAt,
            };

            using var request = CreateRequest(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PanelPointsRecord>(json);
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.TryAddWithoutValidation("X-Api-Key", ModApi.AppSettings.PanelApiKey);
            request.Headers.TryAddWithoutValidation("X-Server-Id", ModApi.AppSettings.ServerId);
            return request;
        }

        private static string NormalizeBaseUrl(string panelUrl)
        {
            return panelUrl.TrimEnd('/');
        }
    }
}
