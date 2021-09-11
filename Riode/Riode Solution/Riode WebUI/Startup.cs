using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            services.AddControllersWithViews()
                //eger productla branddaki kimi referens ozu ozunu cagirirsa
                .AddNewtonsoftJson(cfg=>
                
                    cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );


            services.AddDbContext<RiodeDbContext>(cfg=> {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));
            }, ServiceLifetime.Scoped);

            //butun urller kicik herfli olsun
            services.AddRouting(cfg => cfg.LowercaseUrls = true);


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
                      name: "areas",
                      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute("default","{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
