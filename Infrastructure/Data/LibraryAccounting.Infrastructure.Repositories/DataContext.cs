using System;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, int>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole[]
            {
                new ApplicationUserRole(){ Id = 1, Name = "client"},
                new ApplicationUserRole(){ Id = 2, Name = "librarian"},
                new ApplicationUserRole(){ Id = 3, Name = "admin"}
            });
            mb.Entity<ApplicationUser>().HasData(new ApplicationUser[]
            {
                new ApplicationUser(){ Id = 1, UserName = "Ivan", Email = "Ivan@gmail.com", Password = "sdfgDs23de", RoleId = 2},
                new ApplicationUser(){ Id = 2, UserName = "Danil", Email = "Danil@gmail.com", Password = "e23D23df32", RoleId = 3},
                new ApplicationUser(){ Id = 3, UserName = "Denis", Email = "Denis@gmail.com", Password = "Fd3D23d32r4", RoleId = 1},
                new ApplicationUser(){ Id = 4, UserName = "Vanya", Email = "Vanek@gmail.com", Password = "Dgf34eR34r34r4", RoleId = 1},
                new ApplicationUser(){ Id = 5, UserName = "Dmitry", Email = "DemRh@gmail.com", Password = "DE32f34r", RoleId = 1},
            });
            mb.Entity<Book>().HasData(new Book[]
            {
            new Book() { Id = 1, Title = "Подсознание может все!", Author = "Кехо Джон", Genre = "Психология", Publisher = "Попурри" },
                new Book() { Id = 2, Title = "История", Author = "Некто", Genre = "Наука", Publisher = "Москва" },
                new Book() { Id = 3, Title = "Биология", Author = "Некто", Genre = "Наука", Publisher = "Москва" },
                new Book() { Id = 4, Title = "Химия", Author = "Некто", Genre = "Наука", Publisher = "Питер" },
                new Book()
                {
                    Id = 5,
                    Title = "Семь навыков высокоэффективных людей.",
                    Author = "Стивен Кови",
                    Genre = "Книги по личностному росту от Стивена Кови",
                    Publisher = "Альпина Паблишер"
                },
                new Book()
                {
                    Id = 6,
                    Title = "Семьдесят богатырей",
                    Author = "А. Ивич; Рис. Э. Беньяминсона, Б. Кыштымова",
                    Genre = "Детская литература",
                    Publisher = "Москва"
                },
                new Book()
                {
                    Id = 7,
                    Title = "Периодическая система химических элементов",
                    Author = "Д.И. Менделеев",
                    Genre = "Наука",
                    Publisher = "АСТ"
                },
            });
            mb.Entity<Booking>().HasData(new Booking[]
            {
                new Booking() { Id = 1, BookId = 3, ClientId = 3, BookingDate = DateTime.Now, IsTransmitted = false, IsReturned = false}
            });
            mb.Ignore<IdentityUserLogin<string>>();
            mb.Ignore<IdentityUserClaim<string>>();
            mb.Ignore<IdentityUserToken<string>>();

            base.OnModelCreating(mb);
        }
    }
    //public class BloggingContextFactory : IDesignTimeDbContextFactory<DataContext>
    //{
    //    public DataContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
    //            op => op.MigrationsAssembly("LibraryAccounting.Infrastructure.Repositories"));

    //        return new DataContext(optionsBuilder.Options);
    //    }
    //}
}
