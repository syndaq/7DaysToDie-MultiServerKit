using SdtdMultiServerKit.Functions;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;
using System.Text;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// Backup
    /// </summary>
    [Authorize]
    [RoutePrefix("api/AutoBackup")]
    public class AutoBackupController : ApiController
    {
        /// <summary>
        /// Get backup file list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public List<BackupFileResult> Get()
        {
            if(FunctionManager.TryGetFunction<AutoBackup>(out var function) == false)
            {
                return new List<BackupFileResult>();
            }
            var settings = function!.Settings;

            var result = new List<BackupFileResult>();
            string path = Path.Combine(AppContext.BaseDirectory, settings.ArchiveFolder);
            if (Directory.Exists(path) == false)
            {
                return result;
            }

            // Get all files in the specified directory zip File
            string[] files = Directory.GetFiles(path, "*.zip");

            // All discovered file paths
            foreach (string fileName in files)
            {
                var fileInfo = new FileInfo(fileName);
                var nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name);
                string[] strings = nameWithoutExtension.Split('_');

                if(strings.Length != 5)
                {
                    continue;
                }

                result.Add(new BackupFileResult()
                {
                    CreatedAt = fileInfo.CreationTime,
                    Size = fileInfo.Length,
                    Name = fileInfo.Name,
                    ServerVersion = strings[0],
                    GameWorld = strings[1],
                    GameName = strings[2],
                    Days = strings[3].Substring(3).ToInt(),
                    Hours = strings[4].Substring(4).ToInt(),
                });
            }

            return result;
        }

        /// <summary>
        /// Deletebackupfile
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string[] fileNames)
        {
            if (FunctionManager.TryGetFunction<AutoBackup>(out var function) == false)
            {
                return BadRequest();
            }
            var settings = function!.Settings;
            foreach (var fileName in fileNames)
            {
                string path = Path.Combine(AppContext.BaseDirectory, settings.ArchiveFolder, fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            
            return Ok();
        }

        /// <summary>
        /// Manualbackup
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post()
        {
            if(FunctionManager.TryGetFunction<AutoBackup>(out var autoBackup))
            {
                autoBackup!.ManualBackup();
            }

            return Ok();
        }
    }
}
