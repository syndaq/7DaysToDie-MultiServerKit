using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// Global settings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/GlobalSettings")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class GlobalSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public GlobalSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<GlobalSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] GlobalSettings model)
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
        public GlobalSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<GlobalSettings>(Locales.Get(language));
            return data;
        }
    }
}