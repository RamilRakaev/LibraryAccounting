using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Microsoft.Extensions.Logging;
using System;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookCatalogModel : PageModel
    {
        readonly private IWebHostEnvironment _environment;
        readonly private IMediator _mediator;
        readonly private ILogger<BookCatalogModel> _logger;
        public List<Book> Books { get; private set; }
        public SelectList Authors { get; private set; }
        public SelectList Genres { get; private set; }
        public SelectList Publishers { get; private set; }

        public BookCatalogModel(IWebHostEnvironment environment,
            IMediator mediator,
            ILogger<BookCatalogModel> logger)
        {
            _environment = environment;
            _mediator = mediator;
            _logger = logger;
        }

        private async Task GetSelectLists()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            Authors = new SelectList(authors, "Id", "Name");

            var genres = await _mediator.Send(new GetGenresQuery());
            Genres = new SelectList(genres, "Id", "Name");

            var publishers = _mediator.Send(new GetBooksQuery()).Result.Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
        }

        public async Task OnGet()
        {
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
            _logger.LogInformation($"BookCatalog page visited");
        }

        public async Task OnGetRemove(int id)
        {
            var book = await _mediator.Send(new RemoveBookCommand(id));
            string path = "/img/" + book.Title + ".jpg";
            FileInfo file = new(_environment.WebRootPath + path);
            file.Delete();
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
            _logger.LogInformation($"Book {book.Title} is removed");
        }

        public async Task OnPost(int authorId, int genreId, string publisher)
        {
            Books = await _mediator
                .Send(new GetBooksQuery()
                {
                    AuthorId = authorId,
                    GenreId = genreId,
                    Publisher = publisher
                });
            await GetSelectLists();
            _logger.LogInformation($"Books filter out");
        }

        public async Task OnPostTransfer(int id)
        {
            await _mediator.Send(
                new TransmissionAndAcceptanceBookCommand()
                { Id = id, IsTransfer = true },
                CancellationToken.None);
            Books = await _mediator.Send(new GetBooksQuery());
            await GetSelectLists();
            _logger.LogInformation($"Book transferred");
        }

        public async Task OnPostReturn(int id)
        {
            await _mediator.Send(
                new TransmissionAndAcceptanceBookCommand()
                { Id = id },
                CancellationToken.None);
            Books = await _mediator.Send(new GetBooksQuery());
            await GetSelectLists();
            _logger.LogInformation($"Book received");
        }
    }
}
