using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class Specification:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<SpecificationCategoryItem> SpecificationCategoryItems { get; set; }
    }
}
