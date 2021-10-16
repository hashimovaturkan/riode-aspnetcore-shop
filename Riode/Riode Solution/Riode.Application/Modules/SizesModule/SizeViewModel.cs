using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Application.Modules.SizesModule
{
    public class SizeViewModel
    {
        public long? Id { get; set; }
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
