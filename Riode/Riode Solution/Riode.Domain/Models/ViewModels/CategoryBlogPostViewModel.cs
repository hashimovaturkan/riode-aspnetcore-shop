using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.ViewModels
{
    public class CategoryBlogPostViewModel
    {
        public List<Category> Categories { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
