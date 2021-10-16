using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.ViewModels
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Color> Colors { get; set; }
        public List<Product> Products { get; set; }
    }
}
