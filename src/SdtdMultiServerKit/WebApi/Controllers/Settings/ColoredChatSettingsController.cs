using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// Colored Chat Settings Controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/ColoredChat")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class ColoredChatSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public ColoredChatSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<ColoredChatSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] ColoredChatSettings model)
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
        public ColoredChatSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<ColoredChatSettings>(Locales.Get(language));
            return data;
        }
    }
}