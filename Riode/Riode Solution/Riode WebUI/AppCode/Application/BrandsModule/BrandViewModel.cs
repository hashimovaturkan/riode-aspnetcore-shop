using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BrandsModule
{
    public class BrandViewModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
