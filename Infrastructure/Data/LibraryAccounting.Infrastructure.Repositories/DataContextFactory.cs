using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibraryAccounting.Infrastructure.Repositories
{
    //public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    //{
    //    public DataContext CreateDbContext(string[] args)
    //    {
    //        var builder = new ConfigurationBuilder();
    //        builder.SetBasePath(Directory.GetCurrentDirectory());
    //        builder.AddJsonFile("appsettings.Development.json");
    //        IConfiguration Configuration = builder.Build();
    //        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilder.UseNpgsql(
    //            Configuration.GetConnectionString("DefaultConnection"));

    //        return new DataContext(optionsBuilder.Options);
    //    }
    //}
}
