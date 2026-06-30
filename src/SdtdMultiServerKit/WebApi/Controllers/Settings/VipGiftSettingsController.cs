using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// VIPSettings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/VipGift")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class VipGiftSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public VipGiftSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<VipGiftSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] VipGiftSettings model)
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
        public VipGiftSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<VipGiftSettings>(Locales.Get(language));
            return data;
        }
    }
}