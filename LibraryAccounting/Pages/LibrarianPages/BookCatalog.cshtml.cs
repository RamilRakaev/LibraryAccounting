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
using MediatR;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;
using System.Threading;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookCatalogModel : PageModel
    {
        readonly ILibrarianTools _librarianTools;
        readonly private IWebHostEnvironment _environment;
        readonly private IMediator _mediator;
        public Dictionary<Book, bool> Books { get; private set; }
        public Dictionary<int, ApplicationUser> Users { get; private set; }
        public Dictionary<int, Booking> Bookings { get; private set; }
        public SelectList Authors { get; set; }
        public SelectList Genres { get; set; }
        public SelectList Publishers { get; set; }

        public BookCatalogModel(ILibrarianTools librarianTools, IWebHostEnvironment environment, IMediator mediator)
        {
            _librarianTools = librarianTools;
            _environment = environment;
            _mediator = mediator;

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

        public async Task OnPostTransfer(int id)
        {
            await _mediator.Send(new TransmissionAndAcceptanceBookCommand() { Id = id, IsTransfer = true }, CancellationToken.None);
            await Initialize();
        }

        public async Task OnPostReturn(int id)
        {
            await _mediator.Send(new TransmissionAndAcceptanceBookCommand() { Id = id }, CancellationToken.None);
            await Initialize();
        }

        private async Task Initialize(IEnumerable<Book> books = null)
        {
            if (books == null)
            {
                books = _librarianTools.GetAllBooks();
            }
            Books = new BookCatalogHandler().Handle(books, _librarianTools.GetAllBookings());
            Users = await ExtractUsers(_librarianTools);
            Bookings = await ExtractBookings(_librarianTools);
        }

        static Task<Dictionary<int, ApplicationUser>> ExtractUsers(ILibrarianTools librarianTools)
        {
            return Task.Run(() => new UsersWidthIdHandler().Handle(librarianTools.GetAllUsers()));
        }

        static Task<Dictionary<int, Booking>> ExtractBookings(ILibrarianTools librarianTools)
        {
            return Task.Run(() => new BookingsWidthBookIdHandler().Handle(librarianTools.GetAllBookings()));
        }
    }
}
