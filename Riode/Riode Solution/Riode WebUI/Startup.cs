using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<RiodeDbContext>(cfg=> { }, ServiceLifetime.Scoped);

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

                endpoints.MapControllerRoute("default","{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
