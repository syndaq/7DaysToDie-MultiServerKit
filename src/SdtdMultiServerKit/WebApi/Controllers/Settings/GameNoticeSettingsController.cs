using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// GameSettings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/GameNotice")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class GameNoticeSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public GameNoticeSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<GameNoticeSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] GameNoticeSettings model)
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
        public GameNoticeSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<GameNoticeSettings>(Locales.Get(language));
            return data;
        }
    }
}