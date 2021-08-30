using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;

namespace Infrastructure.Visitors
{
    public class GetBookFromClientVisitor : IVisitor<Booking>
    {
        public bool Visit(Booking element)
        {
            element.IsReturned = true;
            element.ReturnDate = DateTime.Now;
            return true;
        }
    }

    public class GiveBookToClientVisitor : IVisitor<Booking>
    {
        public bool Visit(Booking element)
        {
            element.IsTransmitted = true;
            element.TransferDate = DateTime.Now;
            return true;
        }
    }
}
