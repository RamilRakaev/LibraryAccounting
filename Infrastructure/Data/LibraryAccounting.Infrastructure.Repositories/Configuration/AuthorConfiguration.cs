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
                new Author(){ Id = 1, Name = "Некто"},
                new Author(){ Id = 2, Name = "Кехо Джон"},
                new Author(){ Id = 3, Name = "А. Ивич; Рис. Э. Беньяминсона, Б. Кыштымова"},
                new Author(){ Id = 4, Name = "Д.И. Менделеев"},
                new Author(){ Id = 5, Name = "Стивен Кови"},
            });
        }
    }
}
