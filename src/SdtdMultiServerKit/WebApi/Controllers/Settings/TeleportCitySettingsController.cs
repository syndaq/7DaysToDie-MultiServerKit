using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// Settings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/TeleportCity")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class TeleportCitySettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public TeleportCitySettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<TeleportCitySettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] TeleportCitySettings model)
        {
            ConfigManager.Update(model);
            return Ok();
        }

        /// <summary>
        /// ResetSettings
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public TeleportCitySettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<TeleportCitySettings>(Locales.Get(language));
            return data;
        }
    }
}