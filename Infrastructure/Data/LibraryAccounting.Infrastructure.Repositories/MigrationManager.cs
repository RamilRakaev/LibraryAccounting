using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var appContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    string path = Environment.CurrentDirectory + "/error log.txt";
                    File.AppendAllText(path, $"Message: {ex.Message} StackTrace: {ex.StackTrace}\n");
                    throw;
                }
            }

            return host;
        }
    }
}
