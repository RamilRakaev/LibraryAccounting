using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(b => b.Book)
                .WithOne(b => b.Booking)
                .HasForeignKey<Booking>(b => b.BookId);

            builder.HasOne(b => b.Client)
                .WithOne(c => c.Booking)
                .HasForeignKey<Booking>(b => b.ClientId);
            builder.HasData(new Booking[]
            {
                new Booking() { Id = 1, BookId = 3, ClientId = 3, BookingDate = DateTime.Now, IsTransmitted = false, IsReturned = false}
            });
        }
    }
}
