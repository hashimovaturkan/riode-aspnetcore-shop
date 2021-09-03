using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public long? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long? DeletedByUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
