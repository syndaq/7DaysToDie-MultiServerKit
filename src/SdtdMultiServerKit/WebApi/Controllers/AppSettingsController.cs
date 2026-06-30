using System.Text;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// App Settings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/AppSettings")]
    public class AppSettingsController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(AppSettingsPublic))]
        public AppSettingsPublic Get()
        {
            return AppSettingsPublic.FromAppSettings(ModApi.AppSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put([FromBody] AppSettings appSettings)
        {
            if (ModApi.AppSettings.ApiOnly)
            {
                appSettings.PanelApiKey = ModApi.AppSettings.PanelApiKey;
                appSettings.UserName = ModApi.AppSettings.UserName;
                appSettings.Password = ModApi.AppSettings.Password;
            }

            ValidateAppSettings(appSettings);
            string json = JsonConvert.SerializeObject(appSettings, ModApi.JsonSerializerSettings);
            string path = Path.Combine(ModApi.ModInstance.Path, "Config", "appsettings.json");
            File.WriteAllText(path, json, Encoding.UTF8);
            return Ok(AppSettingsPublic.FromAppSettings(appSettings));
        }

        private static void ValidateAppSettings(AppSettings appSettings)
        {
            if (appSettings.ApiOnly && string.IsNullOrWhiteSpace(appSettings.PanelApiKey))
            {
                throw new ArgumentException("PanelApiKey is required when ApiOnly is enabled.");
            }
        }
    }
}
