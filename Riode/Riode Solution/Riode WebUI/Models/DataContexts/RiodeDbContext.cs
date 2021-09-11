using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.DataContexts
{
    public class RiodeDbContext: DbContext
    {
        public RiodeDbContext():base()
        {

        }

        public RiodeDbContext(DbContextOptions options):base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS; Initial Catalog=Riode; User Id=sa; Password=query;");
        //    }

        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSizeColorItem> ProductSizeColorCollection { get; set; }
        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Faq> Faqs { get; set; }


    }
}
