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
        public DbSet<Author> Authors { get; set; }
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
            //mb.Entity<ApplicationUser>().HasData(new ApplicationUser[]
            //{
            //    new ApplicationUser(){ Id = 1, UserName = "Ivan", Email = "Ivan@gmail.com", 
            //        PasswordHash = "sdfgDs23de", RoleId = 2, EmailConfirmed = true},
            //    new ApplicationUser(){ Id = 2, UserName = "Danil", Email = "Danil@gmail.com", 
            //        Password = "e23D23df32", RoleId = 3, EmailConfirmed = true},
            //    new ApplicationUser(){ Id = 3, UserName = "Denis", Email = "Denis@gmail.com", 
            //        Password = "Fd3D23d32r4", RoleId = 1, EmailConfirmed = true},
            //    new ApplicationUser(){ Id = 4, UserName = "Vanya", Email = "Vanek@gmail.com", 
            //        Password = "Dgf34eR34r34r4", RoleId = 1, EmailConfirmed = true},
            //    new ApplicationUser(){ Id = 5, UserName = "Dmitry", Email = "DemRh@gmail.com", 
            //        Password = "DE32f34rf38jL", RoleId = 1, EmailConfirmed = true},
            //});

            mb.Ignore<IdentityUserLogin<string>>();
            mb.Ignore<IdentityUserClaim<string>>();
            mb.Ignore<IdentityUserToken<string>>();

            mb.ApplyConfiguration(new BookingConfiguration());
            mb.ApplyConfiguration(new BookConfiguration());
            mb.ApplyConfiguration(new GenreConfiguration());
            mb.ApplyConfiguration(new AuthorConfiguration());
            base.OnModelCreating(mb);
        }
    }
}
