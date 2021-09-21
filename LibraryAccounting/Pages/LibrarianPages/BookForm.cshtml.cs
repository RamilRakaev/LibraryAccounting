using System;
using System.IO;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Model;
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
        readonly private IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        readonly private ILogger<BookCatalogModel> _logger;
        public Book Book { get; private set; }
        public SelectList Genres { get; set; }
        public SelectList Authors { get; set; }

        public BookFormModel(IWebHostEnvironment environment,
            IMediator mediator,
            ILogger<BookCatalogModel> logger)
        {
            _environment = environment;
            Book = new Book();
            _mediator = mediator;
            _logger = logger;
            Genres = new SelectList(_mediator.Send(new GetGenresQuery()).Result, "Id", "Name");
            Authors = new SelectList(_mediator.Send(new GetAuthorsQuery()).Result, "Id", "Name");
        }

        public async Task OnGet(int? id)
        {
            _logger.LogInformation($"BookForm page visited");
            if (id != null)
            {
                _logger.LogDebug($"id is not zero");
                Book = await _mediator.Send(new GetBookQuery(Convert.ToInt32(id)));
            }
            _logger.LogDebug($"id is zero");
        }

        public async Task<IActionResult> OnPost(
            Book book,
            IFormFile cover,
            string anotherAuthor,
            string anotherGenre)
        {
            book.AddGenreAndAuthor(anotherAuthor, anotherGenre);
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
                        await _mediator.Send(new AddBookCommand() { Book = book });
                        _logger.LogInformation($"Added Book {book.Title}");
                    }
                    else
                    {
                        await _mediator.Send(new ChangeAllBookPropertiesCommand(Book));
                        _logger.LogInformation($"Сhanged all properties of the book {book.Title}");
                    }
                    return RedirectToPage("/LibrarianPages/BookCatalog");
                }
                else
                {
                    ModelState.AddModelError("", "Вы не добавили изображение");
                }
            return RedirectToPage("/LibrarianPages/BookForm");
        }
    }

    static class BookExtension
    {
        public static void AddGenreAndAuthor(this Book book, string anotherAuthor, string anotherGenre)
        {
            if (string.IsNullOrEmpty(anotherGenre) == false)
            {
                book.Genre = new Genre(anotherGenre);
            }

            if (string.IsNullOrEmpty(anotherAuthor) == false)
            {
                book.Author = new Author(anotherAuthor);
            }
        }
    }
}
