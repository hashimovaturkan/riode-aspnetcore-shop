using AutoMapper;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Riode.Application.Modules.ProductsModule;

namespace Riode.Application.Core.MapperProfilers
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
