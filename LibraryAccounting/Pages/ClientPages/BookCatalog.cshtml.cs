using System;
using System.Collections.Generic;
using System.Linq;
using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookCatalogModel : PageModel
    {
        private readonly IClientTools _clientTools;
        public UserProperties UserProperties;
        public Dictionary<Book, bool> Books { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Genres { get; set; }
        public SelectList Publishers { get; set; }

        public BookCatalogModel(IClientTools clientTools, UserProperties userProperties)
        {
            _clientTools = clientTools;
            UserProperties = userProperties;
            var authors = _clientTools.GetAllBooks().Select(b => b.Author).Distinct();
            Authors = new SelectList(authors);

            var genres = _clientTools.GetAllBooks().Select(b => b.Genre).Distinct();
            Genres = new SelectList(genres);

            var publishers = _clientTools.GetAllBooks().Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
        }

        public async Task OnGet()
        {

            Books = await Task.Run(() => ExtractBooks(_clientTools));
        }

        static Dictionary<Book, bool> ExtractBooks(IClientTools clientTools)
        {
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
        }
    }
}
