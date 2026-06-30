namespace SdtdMultiServerKit.Models
{
    public class AllowedCommand
    {
        /// <summary>
        /// Command
        /// </summary>
        public IEnumerable<string> Commands { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Etc
        /// </summary>
        public int PermissionLevel { get; set; }
    }
}