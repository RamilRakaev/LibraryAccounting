using System.Collections.Generic;
using System.Linq;
using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using MediatR;
using LibraryAccounting.CQRSInfrastructure.Methods.BookMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.AuthorMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.GenreMethods;
using Microsoft.AspNetCore.Hosting;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookCatalogModel : PageModel
    {
        readonly private IMediator _mediator;
        readonly private ILogger<BookCatalogModel> _logger;
        public List<Book> Books { get; private set; }
        public SelectList Authors { get; private set; }
        public SelectList Genres { get; private set; }
        public SelectList Publishers { get; private set; }
        public new UserProperties User { get; set; }

        public BookCatalogModel(
            IMediator mediator,
            ILogger<BookCatalogModel> logger,
            UserProperties user)
        {
            _mediator = mediator;
            _logger = logger;
            User = user;
        }

        private async Task GetSelectLists()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            Authors = new SelectList(authors, "Id", "Name");

            var genres = await _mediator.Send(new GetGenresQuery());
            Genres = new SelectList(genres, "Id", "Name");

            var publishers = _mediator.Send(new GetBooksQuery()).Result.Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
            _logger.LogInformation($"BookCatalog page visited: {DateTime.Now:T}");
        }

        public async Task OnGet()
        {
            if(User.IsAuthenticated == false)
            {
                RedirectToPage("/Account/Login");
            }
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
            _logger.LogInformation($"BookCatalog page visited: {DateTime.Now:T}");
        }

        public async Task OnPost(int? authorId, int? genreId, string publisher)
        {
            Books = await _mediator
                .Send(new GetBooksQuery(
                    authorId,
                    genreId,
                    publisher));
            await GetSelectLists();
            _logger.LogInformation($"Books filtered out: {DateTime.Now:T}");
        }

        public async Task OnPostBooking(int bookId, int clientId)
        {
            await _mediator.Send(new AddBookingCommand(bookId, clientId));
            _logger.LogInformation($"Added booking: {DateTime.Now:T}");
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
        }
    }
}
