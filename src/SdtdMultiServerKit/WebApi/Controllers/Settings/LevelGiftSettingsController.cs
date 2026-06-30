using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    [Authorize]
    [RoutePrefix("api/Settings/LevelGift")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class LevelGiftSettingsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public LevelGiftSettings GetSettings([FromUri] Language language)
        {
            return ConfigManager.GetRequired<LevelGiftSettings>(Locales.Get(language));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] LevelGiftSettings model)
        {
            ConfigManager.Update(model);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public LevelGiftSettings ResetSettings([FromUri] Language language)
        {
            return ConfigManager.LoadDefault<LevelGiftSettings>(Locales.Get(language));
        }
    }
}
