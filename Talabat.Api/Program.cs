using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;
using Talabat.Repositary.DbContexts;
using Talabat.Repositary.Identity;

namespace Talabat.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //update database
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<TalabatContext>();
                await context.Database.MigrateAsync();
                await talabatDbContextSeed.seedAsync(context, loggerFactory);
                var IdentityContext = services.GetRequiredService<AppUserDbContext>();
                await IdentityContext.Database.MigrateAsync();
                var userManager = services.GetRequiredService<UserManager<AppUser>>(); // clr create object form user manger 
                await AppUserDbcontextSeed.CreateAppUser(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occured during migration");
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
