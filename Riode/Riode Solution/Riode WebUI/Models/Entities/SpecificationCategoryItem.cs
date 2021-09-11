using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class SpecificationCategoryItem:BaseEntity
    {
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public long SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
    }
}
