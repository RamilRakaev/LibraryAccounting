using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class DataContext : IdentityDbContext<ApplicationUser, ApplictionUserRole, int>
    {
        public DbSet<Book> Books { get; set; }
        public new DbSet<ApplicationUser> Users { get; set; }
        public new DbSet<ApplictionUserRole> Roles { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            //mb.Entity<Role>().HasData(new Role[]
            //{
            //    new Role(){ Id = 1, Name = "client"},
            //    new Role(){ Id = 2, Name = "librarian"},
            //    new Role(){ Id = 3, Name = "admin"}
            //});
            //mb.Entity<User>().HasData(new User[]
            //{
            //    new User(){ Id = 1, UserName = "Иван", Email = "Ivan@gmail.com", Password = "1234567890", RoleId = 2},
            //    new User(){ Id = 2, UserName = "Данил", Email = "Danil@gmail.com", Password = "1234567890", RoleId = 3},
            //    new User(){ Id = 3, UserName = "Денис", Email = "Denis@gmail.com", Password = "dasf34rfew43", RoleId = 1},
            //    new User(){ Id = 4, UserName = "Ваня", Email = "Vanek@gmail.com", Password = "23534534623423", RoleId = 1},
            //    new User(){ Id = 5, UserName = "Дмитрий", Email = "DemRh@gmail.com", Password = "п54вув324ук", RoleId = 1},
            //});
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
}
