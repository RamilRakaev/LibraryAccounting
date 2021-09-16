using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Infrastructure.Repositories.Configuration
{
    class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasMany(g => g.Authors)
                .WithMany(a => a.Genres)
                .UsingEntity<Book>(b => b.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId),
                b => b.HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                );

            builder.HasData(new Genre[]
            {
                new Genre(){Id = 1, Name = "Наука"},
                new Genre(){Id = 2, Name = "Психология"},
                new Genre(){Id = 3, Name = "Книги по личностному росту от Стивена Кови"},
                new Genre(){Id = 4, Name = "Детская литература"},
                new Genre(){Id = 5, Name = "Книги по личностному росту от Стивена Кови"},
            });
        }
    }
}
