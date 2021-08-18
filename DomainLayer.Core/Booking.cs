﻿using Domain.Interfaces;
using System;

namespace Domain.Model
{
    public class Booking : IElement
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int PersonAccountId { get; set; }
        public bool IsTransmitted { get; set; }
        public bool IsReturned { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Booking()
        {

        }

        public Booking(DateTime transferDate)
        {
            TransferDate = transferDate;
        }
    }
}
