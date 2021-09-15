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

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookCatalogModel : PageModel
    {
        private readonly IClientTools _clientTools;
        private readonly ILogger<BookCatalogModel> _logger;
        public UserProperties UserProperties { get; private set; }
        public Dictionary<Book, bool> Books { get; private set; }
        public SelectList Authors { get; private set; }
        public SelectList Genres { get; private set; }
        public SelectList Publishers { get; private set; }

        public BookCatalogModel(IClientTools clientTools, 
            UserProperties userProperties,
            ILogger<BookCatalogModel> logger)
        {
            _clientTools = clientTools;
            UserProperties = userProperties;
            var authors = _clientTools.GetAllBooks().Select(b => b.Author).Distinct();
            Authors = new SelectList(authors);

            var genres = _clientTools.GetAllBooks().Select(b => b.Genre).Distinct();
            Genres = new SelectList(genres);

            var publishers = _clientTools.GetAllBooks().Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
            _logger = logger;
        }

        public async Task OnGet()
        {
            _logger.LogInformation($"BookCatalog page visited: {DateTime.Now:T}");
            Books = await Task.Run(() => ExtractBooks(_clientTools));
        }

        Dictionary<Book, bool> ExtractBooks(IClientTools clientTools)
        {
            _logger.LogDebug($"Books are retrieved from the database: {DateTime.Now:T}");
            return new BookCatalogHandler().Handle(clientTools.GetAllBooks(), clientTools.GetAllBookings());
        }

        public async Task OnPost(string author, string genre, string publisher)
        {
            var decorator = new DecoratorHandler<Book>(
                new List<IRequestsHandlerComponent<Book>>()
                {new BooksByAuthorHandler(author),
                new BooksByGenreHandler(genre),
                new BooksByPublisherHandler(publisher)
                });

            var books = _clientTools.GetBooks(decorator);
            Books = await Task.Run(() => ExtractBooks(_clientTools));
            _logger.LogInformation($"Books sorted: {DateTime.Now:T}");
        }
    }
}
