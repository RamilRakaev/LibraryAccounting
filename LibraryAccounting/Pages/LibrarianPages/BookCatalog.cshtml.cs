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
using Microsoft.Extensions.Logging;
using System;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookCatalogModel : PageModel
    {
        readonly ILibrarianTools _librarianTools;
        readonly private IWebHostEnvironment _environment;
        readonly private IMediator _mediator;
        readonly private ILogger<BookCatalogModel> _logger;
        public Dictionary<Book, bool> Books { get; private set; }
        public Dictionary<int, ApplicationUser> Users { get; private set; }
        public Dictionary<int, Booking> Bookings { get; private set; }
        public SelectList Authors { get; private set; }
        public SelectList Genres { get; private set; }
        public SelectList Publishers { get; private set; }

        public BookCatalogModel(ILibrarianTools librarianTools,
            IWebHostEnvironment environment,
            IMediator mediator,
            ILogger<BookCatalogModel> logger)
        {
            _librarianTools = librarianTools;
            _environment = environment;
            _mediator = mediator;
            _logger = logger;

            var authors = _librarianTools.GetAllBooks().Select(b => b.Author).Distinct();
            Authors = new SelectList(authors);

            var genres = _librarianTools.GetAllBooks().Select(b => b.Genre).Distinct();
            Genres = new SelectList(genres);

            var publishers = _librarianTools.GetAllBooks().Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
        }

        public async Task OnGet()
        {
            _logger.LogInformation($"BookCatalog page visited: {DateTime.Now:T}");
            await ExctractBooks();
        }

        public async Task OnGetRemove(int id)
        {
            var book = _librarianTools.GetBook(id);
            string path = "/img/" + book.Title + ".jpg";
            FileInfo file = new(_environment.WebRootPath + path);
            file.Delete();
            _librarianTools.RemoveBook(book);
            await ExctractBooks();
            _logger.LogInformation($"Book {book.Title} is removed: {DateTime.Now:T}");
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
            await ExctractBooks(books);
            _logger.LogInformation($"Books sorted: {DateTime.Now:T}");
        }

        public async Task OnPostTransfer(int id)
        {
            await _mediator.Send(new TransmissionAndAcceptanceBookCommand() { Id = id, IsTransfer = true }, CancellationToken.None);
            await ExctractBooks();
            _logger.LogInformation($"Book transferred: {DateTime.Now:T}");
        }

        public async Task OnPostReturn(int id)
        {
            await _mediator.Send(new TransmissionAndAcceptanceBookCommand() { Id = id }, CancellationToken.None);
            _logger.LogInformation($"Book received: {DateTime.Now:T}");
            await ExctractBooks();
        }

        private async Task ExctractBooks(IEnumerable<Book> books = null)
        {
            if (books == null)
            {
                books = _librarianTools.GetAllBooks();
            }
            Books = new BookCatalogHandler().Handle(books, _librarianTools.GetAllBookings());
            Users = await ExtractUsers(_librarianTools);
            Bookings = await ExtractBookings(_librarianTools);
            _logger.LogInformation($"Data retrieved from database: {DateTime.Now:T}");
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
