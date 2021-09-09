using System;
using System.IO;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookFormModel : PageModel
    {
        readonly private ILibrarianTools LibrarianTools;
        readonly private IWebHostEnvironment Environment;
        private readonly IMediator _mediator;
        public Book Book { get; set; }
        public BookFormModel(ILibrarianTools librarianTools, IWebHostEnvironment environment, IMediator mediator)
        {
            LibrarianTools = librarianTools;
            Environment = environment;
            Book = new Book();
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                Book = LibrarianTools.GetBook(Convert.ToInt32(id));
            }
        }

        public async Task<IActionResult> OnPost(Book book, IFormFile cover)
        {
            if (cover != null)
                if (ModelState.IsValid)
                {
                    if (book.Id == 0)
                    {
                        string path = "/img/" + book.Title + ".jpg";
                        using (var fileStream = new FileStream(Environment.WebRootPath + path, FileMode.Create))
                        {
                            await cover.CopyToAsync(fileStream);
                        }
                        LibrarianTools.AddBook(book);
                    }
                    //else
                    //    _mediator.Send()
                    return RedirectToPage("/LibrarianPages/BookCatalog");
                }
            return RedirectToPage("/LibrarianPages/BookForm");
        }
    }
}
