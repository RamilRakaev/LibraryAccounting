using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Infrastructure.Handlers;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookCatalogModel : PageModel
    {
        readonly ILibrarianTools LibrarianTools;
        readonly private IWebHostEnvironment Environment;
        public Dictionary<Book, bool> Books;
        public Dictionary<int, User> Users;
        public Dictionary<int, Booking> Bookings;
        public SelectList Authors { get; set; }
        public SelectList Genres { get; set; }
        public SelectList Publishers { get; set; }

        public BookCatalogModel(ILibrarianTools librarianTools, IWebHostEnvironment environment)
        {
            LibrarianTools = librarianTools;
            Environment = environment;

            var authors = LibrarianTools.GetAllBooks().Select(b => b.Author).Distinct();
            Authors = new SelectList(authors);

            var genres = LibrarianTools.GetAllBooks().Select(b => b.Genre).Distinct();
            Genres = new SelectList(genres);

            var publishers = LibrarianTools.GetAllBooks().Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
        }

        public void OnGet()
        {
            Initialize();
        }

        public void OnGetRemove(int id)
        {
            var book = LibrarianTools.GetBook(id);
            string path = "/img/" + book.Title + ".jpg";
            FileInfo file = new FileInfo(Environment.WebRootPath + path);
            file.Delete();
            LibrarianTools.RemoveBook(book);
            Initialize();
        }

        public void OnPost(string author, string genre, string publisher)
        {
            var decorator = new DecoratorHandler<Book>(
                new List<IRequestsHandlerComponent<Book>>()
                {new BooksByAuthorHandler(author),
                new BooksByGenreHandler(genre),
                new BooksByPublisherHandler(publisher)
                });

            var books = LibrarianTools.GetBooks(decorator);
            Initialize(books);
        }

        private void Initialize(IEnumerable<Book> books = null)
        {
            if (books == null)
            {
                books = LibrarianTools.GetAllBooks();
            }
            Books = new BookCatalogHandler().Handle(books, LibrarianTools.GetAllBookings());
            Users = new UsersWidthIdHandler().Handle(LibrarianTools.GetAllUsers());
            Bookings = new BookingsWidthBookIdHandler().Handle(LibrarianTools.GetAllBookings());
        }
    }
}
