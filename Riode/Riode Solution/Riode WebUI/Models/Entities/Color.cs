using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class Color:BaseEntity
    {
        public string HexCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
