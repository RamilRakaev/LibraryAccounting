using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAccounting.Infrastructure.Repositories.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasData(new BookAuthor[]
            {
                new BookAuthor("Некто"){ Id = 1},
                new BookAuthor("Кехо Джон"){ Id = 2},
                new BookAuthor("А. Ивич; Рис. Э. Беньяминсона, Б. Кыштымова"){ Id = 3},
                new BookAuthor("Д.И. Менделеев"){ Id = 4},
                new BookAuthor("Стивен Кови") { Id = 5},
            });
        }
    }
}
