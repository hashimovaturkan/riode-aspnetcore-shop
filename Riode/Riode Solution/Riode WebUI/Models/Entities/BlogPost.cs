using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class BlogPost:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public ICollection<BlogImage> Images { get; set; }
        public long? CategoryId{ get; set; }
        public virtual Category Category { get; set; }

    }
}
