using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.CategoriesModule
{
    public class CategoryViewModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Description { get; set; }
    }
}
