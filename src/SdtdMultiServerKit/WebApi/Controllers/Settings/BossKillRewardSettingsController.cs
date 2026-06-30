using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    [Authorize]
    [RoutePrefix("api/Settings/BossKillReward")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class BossKillRewardSettingsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public BossKillRewardSettings GetSettings([FromUri] Language language)
        {
            return ConfigManager.GetRequired<BossKillRewardSettings>(Locales.Get(language));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] BossKillRewardSettings model)
        {
            ConfigManager.Update(model);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public BossKillRewardSettings ResetSettings([FromUri] Language language)
        {
            return ConfigManager.LoadDefault<BossKillRewardSettings>(Locales.Get(language));
        }
    }
}
