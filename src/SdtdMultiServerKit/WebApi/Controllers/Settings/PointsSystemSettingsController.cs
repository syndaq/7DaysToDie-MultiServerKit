using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// PointsSettings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/PointsSystem")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class PointsSystemSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public PointsSystemSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<PointsSystemSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] PointsSystemSettings model)
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
        public PointsSystemSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<PointsSystemSettings>(Locales.Get(language));
            return data;
        }
    }
}