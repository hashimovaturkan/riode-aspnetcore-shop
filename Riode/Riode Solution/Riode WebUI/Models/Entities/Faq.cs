using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class Faq:BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
