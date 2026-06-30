using NSwag.Annotations;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    [Authorize]
    [RoutePrefix("api/Settings/PvpArea")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class PvpAreaSettingsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public PvpAreaSettings GetSettings([FromUri] Language language)
        {
            return ConfigManager.GetRequired<PvpAreaSettings>(Locales.Get(language));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] PvpAreaSettings model)
        {
            ConfigManager.Update(model);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public PvpAreaSettings ResetSettings([FromUri] Language language)
        {
            return ConfigManager.LoadDefault<PvpAreaSettings>(Locales.Get(language));
        }
    }
}
