using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdtdMultiServerKit.Models
{
    public class CommandList
    {
        public required string Command { get; set; }

        public bool InMainThread { get; set; }

        public string? Description { get; set; }
    }
}
