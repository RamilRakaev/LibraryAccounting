using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    }
}
