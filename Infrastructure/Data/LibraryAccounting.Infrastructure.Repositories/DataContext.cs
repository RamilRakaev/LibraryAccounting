using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Role>().HasData(new Role[]
            {
                new Role(){ Id = 1, Name = "client"},
                new Role(){ Id = 2, Name = "librarian"},
                new Role(){ Id = 3, Name = "admin"}
            });
        }
    }
}
