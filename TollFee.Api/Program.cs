using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using TollFee.Api.Models;

namespace TollFee.Api
{
    public class Program
    {
        public static IConfigurationRoot configuration { get; set; }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("customsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = new TollDBContext(new DbContextOptions<TollDBContext> ());
                    var year = configuration.GetValue<int>("ConfigurableYear");
                    new TollSeeding(context).SeedData(year);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
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
