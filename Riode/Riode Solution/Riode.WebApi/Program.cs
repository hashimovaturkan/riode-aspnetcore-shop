using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Riode.Application.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Riode.WebApi
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //var types = typeof(Program).Assembly.GetTypes();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("Riode."))
                .SelectMany(a => a.GetTypes())
                .ToArray();

            Extension.principals = types
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
                    .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                    .Union(types
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                    .SelectMany(type => type.GetMethods())
                    .Where(method => method.IsPublic
                        && !method.IsDefined(typeof(NonActionAttribute))
                        && method.IsDefined(typeof(AuthorizeAttribute)))
                    .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>()))
                    .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                    .SelectMany(a => a.Policy.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    .Distinct()
                    .ToArray();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
