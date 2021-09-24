using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LibraryAccounting.Infrastructure.Repositories.Configuration;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, int>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookAuthor> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole[]
            {
                new ApplicationUserRole(){ Id = 1, Name = "client"},
                new ApplicationUserRole(){ Id = 2, Name = "librarian"},
                new ApplicationUserRole(){ Id = 3, Name = "admin"}
            });

            mb.Ignore<IdentityUserLogin<string>>();
            mb.Ignore<IdentityUserClaim<string>>();
            mb.Ignore<IdentityUserToken<string>>();

            mb.ApplyConfiguration(new BookingConfiguration());
            mb.ApplyConfiguration(new BookConfiguration());
            mb.ApplyConfiguration(new GenreConfiguration());
            mb.ApplyConfiguration(new AuthorConfiguration());
            mb.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(mb);
        }
    }
}
