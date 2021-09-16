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
            new Book() { Id = 1, Title = "Подсознание может все!", AuthorId = 2, GenreId = 2, Publisher = "Попурри" },
                new Book() { Id = 2, Title = "История", AuthorId = 1, GenreId = 1, Publisher = "Москва" },
                new Book() { Id = 3, Title = "Биология", AuthorId = 1, GenreId = 1, Publisher = "Москва" },
                new Book() { Id = 4, Title = "Химия", AuthorId = 1, GenreId = 1, Publisher = "Питер" },
                new Book()
                {
                    Id = 5,
                    Title = "Семь навыков высокоэффективных людей.",
                    AuthorId = 5,
                    GenreId = 5,
                    Publisher = "Альпина Паблишер"
                },
                new Book()
                {
                    Id = 6,
                    Title = "Семьдесят богатырей",
                    AuthorId = 3,
                    GenreId = 4,
                    Publisher = "Москва"
                },
                new Book()
                {
                    Id = 7,
                    Title = "Периодическая система химических элементов",
                    AuthorId = 4,
                    GenreId = 1,
                    Publisher = "АСТ"
                },
            });
        }
    }
}
