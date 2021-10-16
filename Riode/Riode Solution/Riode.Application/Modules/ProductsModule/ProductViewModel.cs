using Riode.Domain.Models.Entities;
using Riode.Domain.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Application.Modules.ProductsModule
{
    public class ProductViewModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string SkuCode { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductSizeColorItem> ProductSizeColorCollections { get; set; }
        public SelectedSpecificationFormModel[] SelectedSpecifications { get; set; }
        public ImageItemFormModel[] Files { get; set; }
    }
}
