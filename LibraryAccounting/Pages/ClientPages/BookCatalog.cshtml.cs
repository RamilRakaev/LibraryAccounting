using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Handlers;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using LibraryAccounting.Infrastructure.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookCatalogModel : PageModel
    {
        public Dictionary<Book, bool> Books;
        readonly private IClientTools ClientTools;
        public BookCatalogModel(IClientTools clientTools)
        {
            ClientTools = clientTools;
        }

        public void OnGet()
        {
            Books = new BookCatalogHandler().Handle(ClientTools.GetAllBooks(), ClientTools.GetAllBookings());
        }
    }
}
