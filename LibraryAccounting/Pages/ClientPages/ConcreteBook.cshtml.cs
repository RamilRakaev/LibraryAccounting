using System;
using System.Linq;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.ClientPages
{
    public class ConcreteBookModel : PageModel
    {
        readonly private IClientTools ClientTools;
        public Book Book;
        public bool IsFree;
        public int UserId;

        public ConcreteBookModel(IClientTools clientTools, IHttpContextAccessor httpContext)
        {
            ClientTools = clientTools;
            UserId = Convert.ToInt32(httpContext.HttpContext.User.Claims.ElementAt(2).Value);
        }

        public void OnGet(int id, bool isBooking)
        {
            Book = ClientTools.GetBook(id);
            IsFree = isBooking;
        }

        public void OnPost(int userId, int bookId)
        {
            ClientTools.AddReservation(new Booking(bookId, userId));
            Book = ClientTools.GetBook(bookId);
            IsFree = false;
        }
    }
}
