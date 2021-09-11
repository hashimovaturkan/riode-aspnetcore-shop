using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class BlogImage:BaseEntity
    {
        public string FileName { get; set; }
        public bool IsMain { get; set; }
        public long BlogId { get; set; }
        public virtual BlogPost Blog { get; set; }
    }
}
