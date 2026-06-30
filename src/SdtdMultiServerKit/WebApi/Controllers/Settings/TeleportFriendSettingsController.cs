using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// FriendteleportSettings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/TeleportFriend")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class TeleportFriendSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public TeleportFriendSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<TeleportFriendSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] TeleportFriendSettings model)
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
        public TeleportFriendSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<TeleportFriendSettings>(Locales.Get(language));
            return data;
        }
    }
}