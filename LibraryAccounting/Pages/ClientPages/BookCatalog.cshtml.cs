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

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookCatalogModel : PageModel
    {
        private readonly IClientTools _clientTools;
        private readonly ILogger<BookCatalogModel> _logger;
        private readonly IMediator _mediator;
        public UserProperties UserProperties { get; private set; }
        public Dictionary<Book, bool> Books { get; private set; }
        public SelectList Authors { get; private set; }
        public SelectList Genres { get; private set; }
        public SelectList Publishers { get; private set; }

        public BookCatalogModel(IClientTools clientTools, 
            UserProperties userProperties,
            ILogger<BookCatalogModel> logger,
            IMediator mediator)
        {
            _clientTools = clientTools;
            UserProperties = userProperties;
            var authors = _mediator.Send(new GetAuthorsQuery()).Result;
            Authors = new SelectList(authors);

            var genres = _clientTools.GetAllBooks().Select(b => b.Genre).Distinct();
            Genres = new SelectList(genres);

            var publishers = _clientTools.GetAllBooks().Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
            _logger = logger;
            _mediator = mediator;
        }

        public async Task OnGet()
        {
            _logger.LogInformation($"BookCatalog page visited: {DateTime.Now:T}");
            Books = await _mediator.Send(new BookAccessDictionaryQuery());
        }

        public async Task OnPost(int? authorId, int? genreId, string publisher)
        {
            Books = await _mediator.Send(new BookAccessDictionaryQuery(authorId, genreId, publisher));
            _logger.LogInformation($"Books sorted: {DateTime.Now:T}");
        }
    }
}
