using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Handlers;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using LibraryAccounting.Infrastructure.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookCatalogModel : PageModel
    {
        public Dictionary<Book, bool> Books { get; set; }
        readonly private IClientTools ClientTools;
        public int ClientId { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Genres { get; set; }
        public SelectList Publishers { get; set; }

        public BookCatalogModel(IClientTools clientTools, IHttpContextAccessor httpContext)
        {
            ClientTools = clientTools;
            if (httpContext.HttpContext.User.Identity.IsAuthenticated)
                ClientId = Convert.ToInt32(httpContext.HttpContext.User.Claims.ElementAt(2).Value);

            var authors = ClientTools.GetAllBooks().Select(b => b.Author).Distinct();
            Authors = new SelectList(authors);

            var genres = ClientTools.GetAllBooks().Select(b => b.Genre).Distinct();
            Genres = new SelectList(genres);

            var publishers = ClientTools.GetAllBooks().Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
        }

        public void OnGet()
        {
            Books = new BookCatalogHandler().Handle(ClientTools.GetAllBooks(), ClientTools.GetAllBookings());
        }

        public void OnPost(string author, string genre, string publisher)
        {
            var decorator = new DecoratorHandler<Book>(
                new List<IRequestsHandlerComponent<Book>>()
                {new BooksByAuthorHandler(author),
                new BooksByGenreHandler(genre),
                new BooksByPublisherHandler(publisher)
                });

            var books = ClientTools.GetBooks(decorator);
            Books = new BookCatalogHandler().Handle(books, ClientTools.GetAllBookings());
        }
    }
}
