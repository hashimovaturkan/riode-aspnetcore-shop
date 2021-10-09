using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.FaqsModule
{
    public class FaqViewModel
    {
        public long? Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
