using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.Entities
{
    public class Size:BaseEntity
    {
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
