using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class SpecificationValue:BaseEntity
    {
        public long SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Value { get; set; }
    }
}
