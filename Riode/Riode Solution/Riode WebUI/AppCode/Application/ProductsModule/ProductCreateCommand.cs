using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.FormModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.ProductsModule
{
    public class ProductCreateCommand : IRequest<long>
    {
        [Required]
        public string Name { get; set; }
        public string SkuCode { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductSizeColorItem> ProductSizeColorCollections { get; set; }
        public SelectedSpecificationFormModel[] SelectedSpecifications { get; set; }
        public ImageItemFormModel[] images { get; set; }

        public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IWebHostEnvironment env;
            readonly IMapper mapper;
            public ProductCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx, IWebHostEnvironment env, IMapper mapper)
            {
                this.env = env;
                this.ctx = ctx;
                this.db = db;
                this.mapper = mapper;
            }
            public async Task<long> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
            {
                if (request.images == null || !request.images.Any(i => i.File != null))
                {
                    ctx.ActionContext.ModelState.AddModelError("Images", "There are not images");
                }

                if (ctx.IsModelStateValid())
                {
                    //eger bos deilse (duzdelt bunu ajaxnan yaz)
                    var specifications = request?.SelectedSpecifications?.Where(s => !string.IsNullOrWhiteSpace(s.Value))?.ToArray();

                    Product product = new Product();
                    product.BrandId = request.BrandId;
                    product.CategoryId = request.CategoryId;
                    product.Images = request.Images;
                    product.Name = request.Name;
                    product.Description = request.Description;
                    product.ShortDescription = request.ShortDescription;
                    product.SkuCode = request.SkuCode;
                    product.ProductSizeColorCollections = request.ProductSizeColorCollections;
                    //product.SpecificationValues = request.SpecificationValues;

                    //var product = mapper.Map<Product>(request);

                    product.Images = new List<ProductImage>();
                    foreach (var image in request.images.Where(i => i.File != null))
                    {
                        string extension = Path.GetExtension(image.File.FileName); //.jpg
                        string imagePath = $"{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid()}{extension}";
                        string physicalPath = Path.Combine(env.ContentRootPath,
                            "wwwroot",
                            "uploads",
                            "images",
                            "product",
                            imagePath);

                        using (var stream = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                        {
                            await image.File.CopyToAsync(stream);
                        }

                        product.Images.Add(new ProductImage
                        {
                            IsMain = image.IsMain,
                            FileName = imagePath
                        });
                    }

                    db.Products.Add(product);
                    await db.SaveChangesAsync(cancellationToken);

                    if(specifications != null)
                    {
                        //query base linq
                        var availableSpecifications = (
                            from sComing in specifications
                            join s in db.Specifications on sComing.Id equals s.Id
                            select sComing
                            ).ToArray();

                        foreach (var specification in availableSpecifications)
                        {
                            db.SpecificationValues.Add(new SpecificationValue
                            {
                                SpecificationId = specification.Id,
                                ProductId = product.Id,
                                Value = specification.Value
                            });
                        }
                        await db.SaveChangesAsync(cancellationToken);
                    }

                    return product.Id;
                }
                return 0;
                

            }
        }
    }
}
