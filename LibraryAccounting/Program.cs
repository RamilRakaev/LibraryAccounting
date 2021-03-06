using LibraryAccounting.Domain.Model;
using LibraryAccounting.Infrastructure.Repositories.Configuration;
using LibraryAccounting.Timer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace LibraryAccounting
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope()) 
                {
                    var services = scope.ServiceProvider;
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await UserInitializer.InitializeAsync(userManager);
                }
                host.Run();
                BookingDeleteShedule.Start();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder.UseStartup<Startup>();
          })
          .ConfigureLogging(logging =>
          {
              logging.ClearProviders();
              logging.SetMinimumLevel(LogLevel.Trace);
          })
          .UseNLog();
    }
}
