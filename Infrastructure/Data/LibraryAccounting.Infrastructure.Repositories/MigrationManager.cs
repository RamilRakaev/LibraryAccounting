using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class MigrationManager : IHostedService, IDisposable
    {
        private readonly IHost _host;
        private readonly ILogger<MigrationManager> _logger;
        private Timer _timer;

        public MigrationManager(IHost host,
            ILogger<MigrationManager> logger)
        {
            _host = host;
            _logger = logger;
        }

        private void MigrateDatabase(object state)
        {
            _logger.LogInformation($"Start timer of MigragionManager: {DateTime.Now:T}");
            using var scope = _host.Services.CreateScope();
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
