using System;
using System.IO;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
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
        public Book Book { get; set; }
        public BookFormModel(ILibrarianTools librarianTools, IWebHostEnvironment environment)
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
            if(cover != null)
            if (ModelState.IsValid)
            {
                string path = "/img/" + book.Title + ".jpg";
                using(var fileStream = new FileStream(Environment.WebRootPath + path, FileMode.Create))
                {
                    await cover.CopyToAsync(fileStream);
                }
                LibrarianTools.AddBook(book);
                return RedirectToPage("/LibrarianPages/BookCatalog");
            }
            return RedirectToPage("/LibrarianPages/BookForm");
        }
    }
}
