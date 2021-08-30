using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Handlers;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookedBooksModel : PageModel
    {
        readonly private IClientTools ClientTools;
        public Dictionary<Booking, Book> BookingBooks;
        public int ClientId { get; set; }
        public BookedBooksModel(IClientTools clientTools, IHttpContextAccessor httpContext)
        {
            ClientTools = clientTools;
            BookingBooks = new Dictionary<Booking, Book>();
            if (httpContext.HttpContext.User.Identity.IsAuthenticated)
                ClientId = Convert.ToInt32(httpContext.HttpContext.User.Claims.ElementAt(2).Value);
            else
            {
                RedirectToPage("/Index");
            }
        }

        public void OnGet()
        {
            Initialize();
        }

        public void OnPost(int idBooking)
        {
            ClientTools.RemoveReservation(idBooking);
            Initialize();
        }

        private void Initialize()
        {
            var clientBookings = ClientTools.GetBookings(new BookingsByClientIdHandler(ClientId)).ToList();
            new SortingByBookingDateHandler().Handle(ref clientBookings);
            var clientBooks = ClientTools.GetBooks(new BookingBooksHandler(clientBookings)).ToList();
            if (clientBookings.Count() == clientBooks.Count())
                for (int i = 0; i < clientBooks.Count(); i++)
                {
                    BookingBooks.Add(clientBookings[i], clientBooks[i]);
                }
        }
    }
}
