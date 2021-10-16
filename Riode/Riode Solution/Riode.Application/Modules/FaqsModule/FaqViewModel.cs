using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Application.Modules.FaqsModule
{
    public class FaqViewModel
    {
        public long? Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
