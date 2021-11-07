using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.Entities
{
    public class BlogPost:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        public long? CategoryId{ get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<BlogPostComment> Comments { get; set; }

    }
}
