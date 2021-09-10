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
                    //TODO: Отладить запись на файл в отдельном проекте
                    string path = Environment.CurrentDirectory + "error log.txt";
                    FileStream fileStream = null;
                    fileStream = File.Open(path, File.Exists(path) ? FileMode.Append : FileMode.OpenOrCreate);

                    using (StreamWriter fs = new StreamWriter(fileStream))
                    {
                        fs.WriteLine($"Message: {ex.Message} StackTrace: {ex.StackTrace}");
                    };
                    fileStream.Close();
                    throw;
                }
            }

            return host;
        }
    }
}
