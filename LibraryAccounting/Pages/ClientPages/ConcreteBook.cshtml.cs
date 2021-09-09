using System;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.ClientPages
{
    public class ConcreteBookModel : PageModel
    {
        readonly private IClientTools _clientTools;
        public Book Book { get; private set; }
        public bool IsFree { get; private set; }
        public int UserId { get; private set; }

        public ConcreteBookModel(IClientTools clientTools, IHttpContextAccessor httpContext)
        {
            _clientTools = clientTools;
            UserId = Convert.ToInt32(httpContext.HttpContext.User.Claims.ElementAt(2).Value);
        }

        public async Task OnGet(int id, bool isBooking)
        {
            Book = await Task.Run(() => ExtractBook(_clientTools, id));
            IsFree = isBooking;
        }

        public async Task OnPost(int userId, int bookId)
        {
            _clientTools.AddReservation(new Booking(bookId, userId));
            Book = await Task.Run(() => ExtractBook(_clientTools, bookId));
            IsFree = false;
        }

        private static Book ExtractBook(IClientTools clientTools, int bookId)
        {
            return clientTools.GetBook(bookId);
        }
    }
}
