using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class ProductImage:BaseEntity
    {
        public string FileName { get; set; }
        public long ProductId { get; set; }
        public bool IsMain { get; set; }
        public virtual Product Product { get; set; }
    }
}
