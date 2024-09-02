using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ananke.Application.DTO
{
    public class DiskDTO
    {
        public string Label { get; set; }
        public long TotalSpace { get; set; }
        public long AvailableSpace { get; set; }
        public string Name { get; internal set; }
    }
}