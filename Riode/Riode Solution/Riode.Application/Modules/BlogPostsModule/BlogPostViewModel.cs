using Microsoft.AspNetCore.Http;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BlogPostsModule
{
    public class BlogPostViewModel
    {
        public IFormFile file { get; set; }
        public string fileTemp { get; set; }
        public long? Id { get; set; }
        //public string Title { get; set; }
        //public string Content { get; set; }
        //public DateTime PublishedDate { get; set; }
        //public string ImageUrl { get; set; }
        //public long? CategoryId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
