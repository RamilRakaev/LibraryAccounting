using System.Collections.Generic;
using Infrastructure.Handlers;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookCatalogModel : PageModel
    {
        readonly ILibrarianTools LibrarianTools;
        public Dictionary<Book, bool> Books;
        public Dictionary<int, User> Users;
        public Dictionary<int, Booking> Bookings;

        public BookCatalogModel(ILibrarianTools librarianTools)
        {
            LibrarianTools = librarianTools;
        }

        public void OnGet()
        {
            Users = new UsersWidthIdHandler().Handle(LibrarianTools.GetAllUsers());
            Bookings = new BookingsWidthBookIdHandler().Handle(LibrarianTools.GetAllBookings());
            Books = new BookCatalogHandler().Handle(LibrarianTools.GetAllBooks(), LibrarianTools.GetAllBookings());
        }
    }
}
