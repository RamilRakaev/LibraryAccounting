﻿using LibraryAccounting.Domain.Interfaces.DataManagement;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        [Column(TypeName = "datetime2")]
        public DateTime BookingDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? TransferDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ReturnDate { get; set; }

        public Booking()
        {

        }

        public Booking(int bookId, int userId)
        {
            BookingDate = DateTime.Now;
            BookId = bookId;
            ClientId = userId;
        }

        public bool Accept(IVisitor<Booking> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
