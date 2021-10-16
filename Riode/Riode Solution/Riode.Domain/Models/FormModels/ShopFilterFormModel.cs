using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.FormModels
{
    public class ShopFilterFormModel
    {
        public List<int> Brands { get; set; }
        public List<int> Colors { get; set; }
        public List<int> Sizes { get; set; }
    }
}
