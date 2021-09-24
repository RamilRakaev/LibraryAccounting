using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryAccounting.Infrastructure.Repositories.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(b => b.Book)
                .WithOne(b => b.Booking)
                .HasForeignKey<Booking>(b => b.BookId);

            builder.HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClientId);

            builder.HasData(new Booking[]
            {
                new Booking() { Id = 1, 
                    BookId = 3, 
                    ClientId = 3, 
                    BookingDate = DateTime.Now, 
                    IsTransmitted = false, 
                    IsReturned = false}
            });
        }
    }

}
