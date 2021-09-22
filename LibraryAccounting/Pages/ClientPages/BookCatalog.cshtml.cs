using System.Collections.Generic;
using System.Linq;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

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
            _logger.LogInformation($"BookCatalog page visited");
        }

        public async Task OnGet()
        {
            if(User.IsAuthenticated == false || User.RoleId != 1)
            {
                RedirectToPage("/Account/Login");
            }
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
            _logger.LogInformation($"BookCatalog page visited");
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

        public async Task OnPostBooking(int bookId, int clientId)
        {
            await _mediator.Send(new AddBookingCommand(bookId, clientId));
            _logger.LogInformation($"Added new booking");
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
        }
    }
}
