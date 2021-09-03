using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class Size:BaseEntity
    {
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
