using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    [Authorize]
    [RoutePrefix("api/Settings/Lottery")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class LotterySettingsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public LotterySettings GetSettings([FromUri] Language language)
        {
            return ConfigManager.GetRequired<LotterySettings>(Locales.Get(language));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] LotterySettings model)
        {
            ConfigManager.Update(model);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public LotterySettings ResetSettings([FromUri] Language language)
        {
            return ConfigManager.LoadDefault<LotterySettings>(Locales.Get(language));
        }
    }
}
