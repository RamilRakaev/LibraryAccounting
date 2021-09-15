using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(new Book[]
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
        }
    }
}
