using LibraryAccounting.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    public class Startup
    {
        public static DbContextOptions OnConfiguring()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();

            optionsBuilder.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnection"),
                op => op.MigrationsAssembly("LibraryAccounting.Infrastructure.Repositories"));
            return optionsBuilder.Options;
        }

        //public static DbContextOptions<DataContext> TestDbContextOptions()
        //{
        //    // Create a new service provider to create a new in-memory database.
        //    var serviceProvider = new ServiceCollection()
        //        .AddEntityFrameworkInMemoryDatabase()
        //        .BuildServiceProvider();

        //    // Create a new options instance using an in-memory database and 
        //    // IServiceProvider that the context should resolve all of its 
        //    // services from.
        //    var builder = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase("InMemoryDb")
        //        .UseInternalServiceProvider(serviceProvider);

        //    return builder.Options;
        //}
    }
}
