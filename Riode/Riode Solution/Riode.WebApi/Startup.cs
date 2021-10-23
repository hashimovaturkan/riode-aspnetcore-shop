using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Riode.Application.Core.Extensions;
using Riode.Application.Core.Provider;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riode.WebApi
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
            services.AddControllers(cfg =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddDbContext<RiodeDbContext>(cfg => {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));  //2.2
            }, ServiceLifetime.Scoped);

            services.AddRouting(cfg => cfg.LowercaseUrls = true);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Riode Shop API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            var asmbls = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("Riode."))
                .ToArray();

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
                cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 3, 0);

                cfg.SignIn.RequireConfirmedEmail = true;
            });

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:validAudience"],
                    ValidIssuer = configuration["JWT:validIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
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
                            h.User.HasClaim(item, "1");
                            //burda nie h.Role.HasClaim elemedikki tekce userde yoxluyurug
                        });
                    });
                }

            });


            services.AddMediatR(asmbls);

            services.AddAutoMapper(asmbls);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Riode Project");
               
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
