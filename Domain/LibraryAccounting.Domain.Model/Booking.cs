using LibraryAccounting.Domain.Interfaces.DataManagement;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAccounting.Domain.Model
{
    public class Booking : IElement<Booking>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }
        [DefaultValue(false)]
        public bool IsTransmitted { get; set; }
        [DefaultValue(false)]
        public bool IsReturned { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }

        public Booking()
        {

        }

        public Booking(int bookId, int userId)
        {
            BookingDate = DateTime.Now;
            BookId = bookId;
            ClientId = userId;
        }
    }
}
