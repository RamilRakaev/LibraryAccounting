using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Handlers;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookedBooksModel : PageModel
    {
        readonly private IClientTools ClientTools;
        private Dictionary<Booking, Book> BookingBooks;

        public BookedBooksModel(IClientTools clientTools)
        {
            ClientTools = clientTools;
            BookingBooks = new Dictionary<Booking, Book>();
        }

        public void OnGet()
        {
            int id = 1;
            var clientBooking = ClientTools.GetBookings(new BookingsByClientIdHandler(id)).ToList();
            new SortingByBookingDateHandler().Handle(ref clientBooking);
            var clientBooks = ClientTools.GetBooks(new BookingBooksHandler(clientBooking)).ToList();
            if (clientBooking.Count() == clientBooks.Count())
                for (int i = 0; i < clientBooks.Count(); i++)
                {
                    BookingBooks.Add(clientBooking[i], clientBooks[i]);
                }
        }
    }
}
