using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Riode_WebUI.AppCode.Middlewares;
using Riode_WebUI.AppCode.Provider;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Riode_WebUI
{
    public class Startup
    {
        readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(cfg=> {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));
            })
                //eger productla branddaki kimi referens ozu ozunu cagirirsa
                .AddNewtonsoftJson(cfg=>
                    cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );

            //bu dependency injectiondi ici ise sql'in adini basqasi rahat deyissin dyedi
            //db context ucun inject standarti var
            services.AddDbContext<RiodeDbContext>(cfg=> {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));
            }, ServiceLifetime.Scoped);

            //service injection etmek ucun
            //services.AddScoped<Classinadi>();


            //butun urller kicik herfli olsun
            services.AddRouting(cfg => cfg.LowercaseUrls = true);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddIdentity<RiodeUser, RiodeRole>()
                .AddEntityFrameworkStores<RiodeDbContext>();

            services.AddScoped<UserManager<RiodeUser>>();
            services.AddScoped<RoleManager<RiodeRole>>();
            services.AddScoped<SignInManager<RiodeUser>>();

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequiredUniqueChars = 1;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredLength = 3;

                cfg.User.RequireUniqueEmail = true;

                cfg.Lockout.MaxFailedAccessAttempts = 3;
                cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0,3,0);
            });

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/signin.html";
                cfg.AccessDeniedPath = "/accessdenied.html";

                cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
                cfg.Cookie.Name = "Riode";
            });

            services.AddAuthentication(); //senin umumiyyetle girmeye selahiyyetin var ya yox

            //senin hara girmeye selahiyyetin var
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("admin.colors.index", p =>
                {
                    p.RequireAssertion(h =>
                    {
                        return h.User.HasClaim(c => c.Type.Equals("admin.colors.index") && c.Value.Equals("1"));
                    });
                });
            });


            services.AddMediatR(Assembly.GetExecutingAssembly());

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            //sadece rootun icindekilerin gorunmesine icaze verir
            app.UseStaticFiles();

            app.SeedMembership();

            app.UseRouting();

            app.UseRequestLocalization(cfg => {

                cfg.AddSupportedUICultures("az", "en");
                cfg.AddSupportedCultures("az", "en");

                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());
            });



            //eger adminden giririkse allow olmayan bi yere admin  signinne gelmesi ucun
            app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated
                && !context.Request.Cookies.ContainsKey("riode")
                && context.Request.RouteValues.TryGetValue("area", out object areaName)
                && areaName.ToString().ToLower().Equals("admin"))
                {
                    var attr = context.GetEndpoint().Metadata.GetMetadata<AllowAnonymousAttribute>();
                    //eger actionin ustunde allowanonymous atributu varsa onda normal nexte dussun yoxdursa o zaman yonlensin signine 
                    if (attr == null)
                    {
                        //context.Request.Path = "/admin/signin.html";
                        context.Response.Redirect("/admin/signin.html");
                        await context.Response.CompleteAsync();
                    }

                }
                await next();
            });


            app.UseAudit();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/coming-soon.html", async (context) =>
                 {
                     using (var sr=new StreamReader("views/static/coming-soon.html"))
                     {
                         context.Response.ContentType = "text/html";
                         await context.Response.WriteAsync(sr.ReadToEnd());
                     }
                 });

                endpoints.MapControllerRoute("admin_signIn", "admin/signin.html",
                    defaults: new
                    {
                        controller = "Account",
                        action = "Login",
                        area = "Admin"
                    });

                endpoints.MapControllerRoute("default_signIn", "signin.html",
                    defaults:new {
                        controller="Account",
                        action="SignIn",
                        area=""
                    });

                endpoints.MapControllerRoute("admin_signOut", "admin/logout.html",
                    defaults: new
                    {
                        controller = "Account",
                        action = "Logout",
                        area = "Admin"
                    });

                endpoints.MapControllerRoute(
                      name: "areas-with-lang",
                      pattern: "{lang}/{area:exists}/{controller=Dashboard}/{action=Index}/{id?}",
                      constraints: new{
                          lang="en|az|ru"
                        }
                    );

                endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute("default-with-lang", "{lang}/{controller=home}/{action=index}/{id?}",
                    constraints: new
                    {
                        lang = "en|az|ru"
                    });
                endpoints.MapControllerRoute("default","{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
