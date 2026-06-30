using NSwag.Annotations;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;

namespace SdtdMultiServerKit.WebApi.Controllers.Settings
{
    /// <summary>
    /// BackupSettings
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Settings/AutoBackup")]
    [OpenApiTag("Settings", Description = "Settings")]
    public class AutoBackupSettingsController : ApiController
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public AutoBackupSettings GetSettings([FromUri] Language language)
        {
            var data = ConfigManager.GetRequired<AutoBackupSettings>(Locales.Get(language));
            return data;
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSettings([FromBody] AutoBackupSettings model)
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
        public AutoBackupSettings ResetSettings([FromUri] Language language)
        {
            var data = ConfigManager.LoadDefault<AutoBackupSettings>(Locales.Get(language));
            return data;
        }
    }
}