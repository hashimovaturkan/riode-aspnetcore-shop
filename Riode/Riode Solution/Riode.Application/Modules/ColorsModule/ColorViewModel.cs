using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Application.Modules.ColorsModule
{
    public class ColorViewModel
    {
        public long? Id { get; set; }
        public string HexCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
