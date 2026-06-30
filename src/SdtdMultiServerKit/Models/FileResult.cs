using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// File
    /// </summary>
    public class FileResult
    {
        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Size { get; set; }
    }
}
