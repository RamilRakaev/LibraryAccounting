using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookCatalogModel : PageModel
    {
        readonly ILibrarianTools _librarianTools;
        readonly private IWebHostEnvironment _environment;
        public Dictionary<Book, bool> Books { get; private set; }
        public Dictionary<int, User> Users { get; private set; }
        public Dictionary<int, Booking> Bookings { get; private set; }
        public SelectList Authors { get; set; }
        public SelectList Genres { get; set; }
        public SelectList Publishers { get; set; }

        public BookCatalogModel(ILibrarianTools librarianTools, IWebHostEnvironment environment)
        {
            _librarianTools = librarianTools;
            _environment = environment;

            var authors = _librarianTools.GetAllBooks().Select(b => b.Author).Distinct();
            Authors = new SelectList(authors);

            var genres = _librarianTools.GetAllBooks().Select(b => b.Genre).Distinct();
            Genres = new SelectList(genres);

            var publishers = _librarianTools.GetAllBooks().Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
        }

        public async Task OnGet()
        {
            await Initialize();
        }

        public async Task OnGetRemove(int id)
        {
            var book = _librarianTools.GetBook(id);
            string path = "/img/" + book.Title + ".jpg";
            FileInfo file = new FileInfo(_environment.WebRootPath + path);
            file.Delete();
            _librarianTools.RemoveBook(book);
            await Initialize();
        }

        public async Task OnPost(string author, string genre, string publisher)
        {
            var decorator = new DecoratorHandler<Book>(
                new List<IRequestsHandlerComponent<Book>>()
                {new BooksByAuthorHandler(author),
                new BooksByGenreHandler(genre),
                new BooksByPublisherHandler(publisher)
                });

            var books = _librarianTools.GetBooks(decorator);
            await Initialize(books);
        }

        private async Task Initialize(IEnumerable<Book> books = null)
        {
            if (books == null)
            {
                books = _librarianTools.GetAllBooks();
            }
            this.Books = new BookCatalogHandler().Handle(books, _librarianTools.GetAllBookings());
            Users = await Task.Run(() => ExtractUsers(_librarianTools));
            Bookings = await Task.Run(() => ExtractBookings(_librarianTools));
        }

        static Dictionary<int, User> ExtractUsers(ILibrarianTools librarianTools)
        {
            return new UsersWidthIdHandler().Handle(librarianTools.GetAllUsers());
        }

        static Dictionary<int, Booking> ExtractBookings(ILibrarianTools librarianTools)
        {
            return new BookingsWidthBookIdHandler().Handle(librarianTools.GetAllBookings());
        }
    }
}
