using System;
using BethanysPieShop.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BethanysPieShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            // Seed the database
            // Ref: https://stackoverflow.com/questions/45782446/unable-to-create-migrations-after-upgrading-to-asp-net-core-2-0
            // https://elanderson.net/2017/09/unable-to-create-an-object-of-type-applicationdbcontext-add-an-implementation-of-idesigntimedbcontextfactory/
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var seeder = services.GetService<DbInitializer>();
                    seeder.Seed();
                }
                catch(Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .Build();
    }
}
