using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookFormModel : PageModel
    {
        readonly private ILibrarianTools LibrarianTools;
        public Book Book { get; set; }
        public BookFormModel(ILibrarianTools librarianTools)
        {
            LibrarianTools = librarianTools;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                Book = LibrarianTools.GetBook(Convert.ToInt32(id));
            }
            else
            {
                Book = new Book();
            }
        }

        public IActionResult OnPost(Book book)
        {
            if (ModelState.IsValid)
            {
                LibrarianTools.AddBook(book);
                return RedirectToPage("/Librarian/BookCatalog");
            }
            return RedirectToPage("/LibrarianPages/BookForm");
        }
    }
}
