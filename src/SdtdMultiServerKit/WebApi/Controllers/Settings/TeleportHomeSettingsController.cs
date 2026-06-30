using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// Settings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/TeleportHome")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class TeleportHomeSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public TeleportHomeSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<TeleportHomeSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] TeleportHomeSettings model)
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
        public TeleportHomeSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<TeleportHomeSettings>(Locales.Get(language));
            return data;
        }
    }
}