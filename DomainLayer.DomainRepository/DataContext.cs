using System.Collections.Generic;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<PersonAccount> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
