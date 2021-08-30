using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.ClientPages
{
    public class ConcreteBookModel : PageModel
    {
        readonly private IClientTools ClientTools;
        public Book Book;
        public bool IsBooking;
        public User UserInfo;

        public ConcreteBookModel(IClientTools clientTools)
        {
            ClientTools = clientTools;
        }

        public void OnGet(int id, bool isBooking)
        {
            Book = ClientTools.GetBook(id);
            IsBooking = isBooking;
        }

        public void OnPost(int userId, int bookId)
        {
            ClientTools.AddReservation(new Booking(bookId, userId));
            Book = ClientTools.GetBook(bookId);
            IsBooking = true;
        }
    }
}
