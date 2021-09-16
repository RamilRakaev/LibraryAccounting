using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAccounting.Infrastructure.Repositories.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(new Author[]
            {
                new Author("Некто"){ Id = 1},
                new Author("Кехо Джон"){ Id = 2},
                new Author("А. Ивич; Рис. Э. Беньяминсона, Б. Кыштымова"){ Id = 3},
                new Author("Д.И. Менделеев"){ Id = 4},
                new Author("Стивен Кови") { Id = 5},
            });
        }
    }
}
