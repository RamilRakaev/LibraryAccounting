using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class MigrationManager : IHostedService, IDisposable
    {
        private readonly IHost _host;
        private Timer _timer;

        public MigrationManager(IHost host)
        {
            _host = host;
        }

        private void MigrateDatabase(object state)
        {
            using (var scope = _host.Services.CreateScope())
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
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(MigrateDatabase, null, TimeSpan.Zero, TimeSpan.FromHours(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
