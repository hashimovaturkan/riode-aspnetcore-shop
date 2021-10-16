using MediatR;
using Microsoft.AspNetCore.Authentication;
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
using Riode.Application.Core.Middlewares;
using Riode.Application.Core.Provider;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Riode.Application.Core.Extensions;

/*
 * 1.  Sistem konfiqurasiyalarının istifadəsi
 * 1.1 Konfiqurasiların inject edilməsi - appsetting.json
 * 1.2 Konfiqurasiyaların Startup class-ı daxilində qloballaşdırılması
 * 
 * 2.  Database-nın konfiqurasiyası
 * 2.1 Database contextin servise scope-da registrasiyası
 * 2.2 İstifadə edilən Database-nın database contextə mənimsədilməsi/set edilməsi
 * 2.3 İnMemory database-nin database context-ə mənimsədilməsi
 * 
 * 3.  Mvc prinsiplərinin tətbiq edilməsi
 * 3.1 Mvc prinsiplərinin controller-view əsaslı işinin realizasiyası 
 * 3.2 Mvc routing prinsiplərinin realizasiyası
 * 
 * 4.  .Net identity prinsiplərinin realizasiyası
 * 4.1 User və rolların identity servisə registrasiyası
 * 4.2 SignİnManager servisinin service scope-a registrasiyası
 * 4.3 UserManager servisinin service scope-a registrasiyası
 * 4.4 UserManager servisinin service scopa-a registrasiyası
 * 
 * 5.  SignalR-in realizasiyası
 * 5.1 SignalR servisinin pipeline-ə əlavə edilməsi
 * 5.2 SignalR Hub-ın pipeline-ə əlavə edilməsi
 * 
 * 6.  MultiLanguage- Çoxdilli versiyanın realizasiyası
 * 6.1 MultiLanguage Providerin pipeline-ə əlavə edilməsi
 * 
 * 7.  Statik faylların istifadəsinə icazə verilməsi
 * 
 * 8.  Custom Binder Provider
 * 8.1 BooleanType CustomBinderProvider-i pipeline-yə qoşmaq
 * 8.2 Dateİnterval CustomBinderProvider-i pipeline-yə qoşmaq
 */

namespace Riode_WebUI
{
    public class Startup
    {
        readonly IConfiguration configuration; //1.2
        public Startup(IConfiguration configuration) //1.1
        {
            this.configuration = configuration;  //1.2
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //3.1
            services.AddControllersWithViews(cfg=> {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());  //8.1

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
            //2.1
            services.AddDbContext<RiodeDbContext>(cfg=> {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));  //2.2
            }, ServiceLifetime.Scoped);

            //service injection etmek ucun
            //services.AddScoped<Classinadi>();


            //butun urller kicik herfli olsun
            services.AddRouting(cfg => cfg.LowercaseUrls = true);

            //IActionContextAccessor'u cagiranda ActionContextAccessor'u singleton seklinde versin
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //4.1
            services.AddIdentity<RiodeUser, RiodeRole>()
                .AddEntityFrameworkStores<RiodeDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<RiodeUser>>(); //4.2
            services.AddScoped<RoleManager<RiodeRole>>();  //4.3
            services.AddScoped<SignInManager<RiodeUser>>();  //4.4
            //realtimeda deyisiklikler tetbiq olunsun
            services.AddScoped<IClaimsTransformation, AppClaimProvider>();

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

                cfg.SignIn.RequireConfirmedEmail = true;
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
                foreach (var item in Extension.principals)
                {
                    cfg.AddPolicy(item, p =>
                    {
                        p.RequireAssertion(h =>
                        {
                            return h.User.IsInRole("SuperAdmin") ||
                            h.User.HasClaim(item,"1") ;
                            //burda nie h.Role.HasClaim elemedikki tekce userde yoxluyurug
                        });
                    });
                }
                
            });

            var asmbls = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("Riode."))
                .ToArray();
            services.AddMediatR(asmbls);

            services.AddAutoMapper(asmbls);

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            //sadece rootun icindekilerin gorunmesine icaze verir
            app.UseStaticFiles();

            //program ilk ayaga qalxabda superadmini falan yaratsin 
            app.SeedMembership();

            app.UseRouting();

            app.UseRequestLocalization(cfg => {

                cfg.AddSupportedUICultures("az", "en");
                cfg.AddSupportedCultures("az", "en");

                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());
            });



            //eger adminden giririkse allow olmayan bi yere admin signinne gelmesi ucun
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
