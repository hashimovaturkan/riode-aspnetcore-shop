using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.DataContexts
{
    public static class RiodeDbSeed
    {
        public static IApplicationBuilder SeedMembership( this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                

                var role = new RiodeRole
                {
                    Name = "SuperAdmin"
                };

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<RiodeUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RiodeRole>>();
                

                bool hasRole = roleManager.RoleExistsAsync(role.Name).Result;   //role axtarilsin
                if (hasRole == true) //eger role varsa
                {
                    role = roleManager.FindByNameAsync(role.Name).Result;  //hemin rolu tap
                }
                else  //eger rol yoxdursa
                {
                    var iResult = roleManager.CreateAsync(role).Result; //hemin rolu yarat
                    if (!iResult.Succeeded)         //eger rol yaradila bilmedise
                        goto end;
                }

                string password = "123";

                var user = new RiodeUser
                {
                    UserName="Turkan",
                    Email = "turkan@mail.ru",
                    EmailConfirmed = true

                };

                var founded = userManager.FindByEmailAsync(user.Email).Result;   // user axtarilsin

                if (founded !=null && ! userManager.IsInRoleAsync(founded,role.Name).Result)  // eger user varsa ve hemin rolda deyilse
                {
                    userManager.AddToRoleAsync(founded, role.Name).Wait();   //useri hemin rola add ele
                }
                else if(founded == null)  // eger user yoxdursa
                {
                    var iUserResult = userManager.CreateAsync(user, password).Result;  // useri yarat

                    if(iUserResult.Succeeded) //eger user yaradildisa
                    {
                        userManager.AddToRoleAsync(user, role.Name).Wait();   //useri hemin rola add ele
                    }
                    else
                    {
                        goto end;
                    }
                }
            }

            end:
            return builder;
        }

        public static IApplicationBuilder Seed(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RiodeDbContext>();
                db.Database.Migrate();

                //eger ilk yarananda isteyirikse bos qalmasin biseyler mesleen bloglar bele edirik
                //InitBlogPosts(db);
            }
            return builder;
        }

        private static void InitBlogPosts(RiodeDbContext db)
        {
            if (!db.BlogPosts.Any())
            {
                db.BlogPosts.Add(new BlogPost
                {
                    Title = "Blog1",
                    Content = "Content1",
                    ImageUrl = "",
                    CategoryId = 0,
                    PublishedDate = DateTime.Now

                });

                db.BlogPosts.Add(new BlogPost
                {
                    Title = "Blog2",
                    Content = "Content2",
                    ImageUrl = "",
                    CategoryId = 0,
                    PublishedDate = DateTime.Now

                });
                db.SaveChanges();
            }
        }
    }
}
