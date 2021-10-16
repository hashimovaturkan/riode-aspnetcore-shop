using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.Entities
{
    public class Color:BaseEntity
    {
        public string HexCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
