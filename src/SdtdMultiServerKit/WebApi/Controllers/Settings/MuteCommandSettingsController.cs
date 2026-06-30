using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    [Authorize]
    [RoutePrefix("api/Settings/MuteCommand")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class MuteCommandSettingsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public MuteCommandSettings GetSettings([FromUri] Language language)
        {
            return ConfigManager.GetRequired<MuteCommandSettings>(Locales.Get(language));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] MuteCommandSettings model)
        {
            ConfigManager.Update(model);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public MuteCommandSettings ResetSettings([FromUri] Language language)
        {
            return ConfigManager.LoadDefault<MuteCommandSettings>(Locales.Get(language));
        }
    }
}
