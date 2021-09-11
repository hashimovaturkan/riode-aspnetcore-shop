using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string SkuCode { get; set; }
        public long BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductSizeColorItem> ProductSizeColorCollections { get; set; }
        public virtual ICollection<SpecificationValue> SpecificationValues { get; set; }

    }
}
