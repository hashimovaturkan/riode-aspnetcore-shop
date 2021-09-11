using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class BlogPost:BaseEntity
    {
        public string Title { get; set; }
        public string WritedBy { get; set; }
        public string Content { get; set; }
        public string Quote { get; set; }
        public ICollection<BlogImage> Images { get; set; }
    }
}
