using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.DataContexts
{
    //public class RiodeDbContext: DbContext
    public class RiodeDbContext : IdentityDbContext<RiodeUser,RiodeRole,long,RiodeUserClaim,RiodeUserRole,RiodeUserLogin,RiodeRoleClaim,RiodeUserToken>
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
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationCategoryItem> SpecificationCategoryCollection { get; set; }
        public DbSet<SpecificationValue> SpecificationValues { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RiodeUser>(e=>{

                e.ToTable("Users", "Membership");
            });

            builder.Entity<RiodeRole>(e => {

                e.ToTable("Roles", "Membership");
            });

            builder.Entity<RiodeRoleClaim>(e => {

                e.ToTable("RoleClaims", "Membership");
            });

            builder.Entity<RiodeUserClaim>(e => {

                e.ToTable("UserClaims", "Membership");
            });

            builder.Entity<RiodeUserLogin>(e => {

                e.ToTable("UserLogins", "Membership");
            });

            builder.Entity<RiodeUserToken>(e => {

                e.ToTable("UserTokens", "Membership");
            });

            builder.Entity<RiodeUserRole>(e => {

                e.ToTable("UserRoles", "Membership");
            });
        }

    }
}
