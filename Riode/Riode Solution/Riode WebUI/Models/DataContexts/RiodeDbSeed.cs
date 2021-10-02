﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Riode_WebUI.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.DataContexts
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
                }
            }

            end:
            return builder;
        }
    }
}