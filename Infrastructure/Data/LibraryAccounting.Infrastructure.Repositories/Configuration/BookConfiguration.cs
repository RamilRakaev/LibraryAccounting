using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAccounting.Infrastructure.Repositories.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            builder.HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);

            builder.HasData(new Book[]
            {
            new Book() { Id = 1, Title = "Подсознание может все!", Author = "Кехо Джон", GenreId = 2, Publisher = "Попурри" },
                new Book() { Id = 2, Title = "История", Author = "Некто", GenreId = 1, Publisher = "Москва" },
                new Book() { Id = 3, Title = "Биология", Author = "Некто", GenreId = 1, Publisher = "Москва" },
                new Book() { Id = 4, Title = "Химия", Author = "Некто", GenreId = 1, Publisher = "Питер" },
                new Book()
                {
                    Id = 6,
                    Title = "Семьдесят богатырей",
                    Author = "А. Ивич; Рис. Э. Беньяминсона, Б. Кыштымова",
                    GenreId = 4,
                    Publisher = "Москва"
                },
                new Book()
                {
                    Id = 7,
                    Title = "Периодическая система химических элементов",
                    Author = "Д.И. Менделеев",
                    GenreId = 1,
                    Publisher = "АСТ"
                },
                new Book()
                {
                    Id = 5,
                    Title = "Семь навыков высокоэффективных людей.",
                    Author = "Стивен Кови",
                    GenreId = 5,
                    Publisher = "Альпина Паблишер"
                },
            });
        }
    }
}
