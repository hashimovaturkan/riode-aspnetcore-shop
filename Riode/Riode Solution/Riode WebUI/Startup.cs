using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Riode_WebUI.AppCode.Middlewares;
using Riode_WebUI.AppCode.Provider;
using Riode_WebUI.Models.DataContexts;
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

            app.UseRouting();

            app.UseRequestLocalization(cfg=> {

                cfg.AddSupportedUICultures("az","en");
                cfg.AddSupportedCultures("az", "en");

                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());
            });

            app.UseAudit();

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
