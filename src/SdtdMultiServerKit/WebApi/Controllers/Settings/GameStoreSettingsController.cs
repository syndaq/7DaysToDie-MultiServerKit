using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// GameSettings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/GameStore")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class GameStoreSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public GameStoreSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<GameStoreSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] GameStoreSettings model)
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
        public GameStoreSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<GameStoreSettings>(Locales.Get(language));
            return data;
        }
    }
}