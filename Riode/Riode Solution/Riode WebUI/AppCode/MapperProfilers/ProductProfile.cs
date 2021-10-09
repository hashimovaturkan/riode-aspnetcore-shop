using AutoMapper;
using Riode_WebUI.AppCode.Application.ProductsModule;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.MapperProfilers
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateCommand, Product>()
                //.ForMember(dest => dest.ShortDescription, src=> src.MapFrom(m=> m.SDesc))
                .ReverseMap();

            CreateMap<ProductUpdateCommand, Product>()
               //.ForMember(dest => dest.ShortDescription, src=> src.MapFrom(m=> m.SDesc))
               .ReverseMap();
        }

    }
}
