using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.Entities
{
    public class Subscribe:BaseEntity
    {
        public string Email{ get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime? EmailConfirmedDate { get; set; }
    }
}
