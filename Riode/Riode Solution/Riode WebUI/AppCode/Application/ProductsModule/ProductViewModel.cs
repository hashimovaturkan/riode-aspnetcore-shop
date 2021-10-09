using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.ProductsModule
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
