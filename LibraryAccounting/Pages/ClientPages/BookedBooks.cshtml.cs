using System;
using System.Collections.Generic;
using System.Linq;
using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookedBooksModel : PageModel
    {
        readonly private IClientTools _clientTools;
        public Dictionary<Booking, Book> armoredBooks;
        public int ClientId { get; set; }
        public BookedBooksModel(IClientTools clientTools, IHttpContextAccessor httpContext)
        {
            _clientTools = clientTools;
            armoredBooks = new Dictionary<Booking, Book>();
            if (httpContext.HttpContext.User.Identity.IsAuthenticated)
                ClientId = Convert.ToInt32(httpContext.HttpContext.User.Claims.ElementAt(2).Value);
            else
            {
                RedirectToPage("/Index");
            }
        }

        public async Task OnGet()
        {
            armoredBooks = await Task.Run(() => ExtractArmoredBooks(_clientTools, ClientId));
        }

        public async Task OnPost(int idBooking)
        {
            _clientTools.RemoveReservation(idBooking);
            armoredBooks = await Task.Run(() =>ExtractArmoredBooks(_clientTools, ClientId));
        }

        private static Dictionary<Booking, Book> ExtractArmoredBooks(IClientTools ClientTools, int clientId)
        {
            var armoredBooks = new Dictionary<Booking, Book>();
            var clientBookings = ClientTools.GetBookings(new BookingsByClientIdHandler(clientId)).ToList();
            new SortingByBookingDateHandler().Handle(ref clientBookings);
            var clientBooks = ClientTools.GetBooks(new BookingBooksHandler(clientBookings)).ToList();
            if (clientBookings.Count() == clientBooks.Count())
                for (int i = 0; i < clientBooks.Count(); i++)
                {
                    armoredBooks.Add(clientBookings[i], clientBooks[i]);
                }
            return armoredBooks;
        }
    }
}
