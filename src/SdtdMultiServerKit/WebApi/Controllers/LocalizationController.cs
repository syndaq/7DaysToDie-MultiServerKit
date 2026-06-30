namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// Localization
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Localization")]
    [ResponseCache(Duration = 7200)]
    public class LocalizationController : ApiController
    {
        /// <summary>
        /// Getlocalizationdictionary
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Dictionary<string, string>))]
        public IHttpActionResult GetLocalization(Language language)
        {
            string _language = language.ToString().ToLower();
            var dict = Localization.dictionary;
            int languageIndex = Array.LastIndexOf(dict["KEY"], _language);

            if (languageIndex < 0)
            {
                return NotFound();
            }

            return Ok(dict.ToDictionary(p => p.Key, p => p.Value[languageIndex]));
        }

        /// <summary>
        /// Get localized string for the specified item
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="language">language</param>
        /// <param name="caseInsensitive"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{key}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetLocalization(string key, [FromUri]Language language, [FromUri] bool caseInsensitive = false)
        {
            return Ok(Utilities.Utils.GetLocalization(key, language, caseInsensitive));
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/KnownLanguages")]
        [ResponseType(typeof(string))]
        public string[] GetKnownLanguages()
        {
            return Localization.dictionary["KEY"];
        }
    }
}
