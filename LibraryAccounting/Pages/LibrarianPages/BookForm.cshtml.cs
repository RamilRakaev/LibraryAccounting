using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.AuthorMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.BookMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.GenreMethods;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookFormModel : PageModel
    {
        readonly private ILibrarianTools _librarianTools;
        readonly private IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        readonly private ILogger<BookCatalogModel> _logger;
        public Book Book { get; private set; }
        public SelectList Genres { get; set; }
        public SelectList Authors { get; set; }

        public BookFormModel(ILibrarianTools librarianTools,
            IWebHostEnvironment environment,
            IMediator mediator,
            ILogger<BookCatalogModel> logger)
        {
            _librarianTools = librarianTools;
            _environment = environment;
            Book = new Book();
            _mediator = mediator;
            _logger = logger;
            Genres = new SelectList(_mediator.Send(new GetGenresQuery()).Result, "Id", "Name");
            Authors = new SelectList(_mediator.Send(new GetAuthorsQuery()).Result, "Id", "Name");
        }

        public void OnGet(int? id)
        {
            _logger.LogInformation($"BookForm page visited: {DateTime.Now:T}");
            if (id != null)
            {
                _logger.LogDebug($"id is not zero: {DateTime.Now:T}");
                Book = _librarianTools.GetBook(Convert.ToInt32(id));
            }
            _logger.LogDebug($"id is zero: {DateTime.Now:T}");
        }

        public async Task<IActionResult> OnPost(Book book, IFormFile cover)
        {
            if (cover != null)
                if (ModelState.IsValid)
                {
                    if (book.Id == 0)
                    {
                        string path = "/img/" + book.Title + ".jpg";
                        using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                        {
                            await cover.CopyToAsync(fileStream);
                        }
                        await _mediator.Send(new AddBookCommand() { Book = book }, CancellationToken.None);
                        _logger.LogInformation($"Added Book {book.Title}: {DateTime.Now:T}");
                    }
                    else
                    {
                        await _mediator.Send(new ChangeAllBookPropertiesCommand() { Book = book }, CancellationToken.None);
                        _logger.LogInformation($"Ñhanged all properties of the book {book.Title}: {DateTime.Now:T}");
                    }
                    return RedirectToPage("/LibrarianPages/BookCatalog");
                }
            return RedirectToPage("/LibrarianPages/BookForm");
        }
    }
}
