using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Microsoft.Extensions.Logging;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Pages.ClientPages;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class BookCatalogModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        private readonly ILogger<BookCatalogModel> _logger;
        private readonly UserProperties _user;
        public List<Book> Books { get; private set; }
        public SelectList Authors { get; private set; }
        public SelectList Genres { get; private set; }
        public SelectList Publishers { get; private set; }
        private readonly string fileReport_path = Directory.GetCurrentDirectory() + "/Librarian report.xlsx";
        public BookCatalogModel(IWebHostEnvironment environment,
            IMediator mediator,
            ILogger<BookCatalogModel> logger,
            UserProperties user)
        {
            _environment = environment;
            _mediator = mediator;
            _logger = logger;
            _user = user;
        }

        private async Task GetSelectLists()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            Authors = new SelectList(authors, "Id", "Name");

            var genres = await _mediator.Send(new GetGenresQuery());
            Genres = new SelectList(genres, "Id", "Name");

            var publishers = _mediator.Send(new GetBooksQuery()).Result.Select(b => b.Publisher).Distinct();
            Publishers = new SelectList(publishers);
        }

        public async Task<IActionResult> OnGet()
        {
            new FileInfo(fileReport_path).Delete();
            if (_user.IsAuthenticated == false || _user.RoleId != 3)
            {
                return RedirectToPage("/Account/Login");
            }
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
            _logger.LogInformation($"BookCatalog page visited");
            return Page();
        }

        public async Task<IActionResult> OnGetRemove(int id)
        {
            if (_user.IsAuthenticated == false)
            {
                return RedirectToPage("/Account/Login");
            }
            var book = await _mediator.Send(new RemoveBookCommand(id));
            //TODO: Проверить работу комманды
            await _mediator.Send(new RemoveImageCommand(book.Title));
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
            _logger.LogInformation($"Book {book.Title} is removed");
            return Page();
        }

        public async Task<IActionResult> OnGetReporting() 
        {
            
            // Тип файла - content-type
            string file_type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            await _mediator.Send(new LibrarianExcelReportCommand() {Path = fileReport_path });
            await GetSelectLists();
            Books = await _mediator.Send(new GetBooksQuery());
            _logger.LogInformation($"Excel bookings report");
            return PhysicalFile(fileReport_path, file_type);
        }

        public async Task OnPost(int authorId, int genreId, string publisher)
        {
            Books = await _mediator
                .Send(new GetBooksQuery()
                {
                    AuthorId = authorId,
                    GenreId = genreId,
                    Publisher = publisher
                });
            await GetSelectLists();
            _logger.LogInformation($"Books filter out");
        }

        public async Task OnPostTransfer(int id)
        {
            await _mediator.Send(
                new TransmissionAndAcceptanceBookCommand()
                { Id = id, IsTransfer = true },
                CancellationToken.None);
            Books = await _mediator.Send(new GetBooksQuery());
            await GetSelectLists();
            _logger.LogInformation($"Book transferred");
        }

        public async Task OnPostReturn(int id)
        {
            await _mediator.Send(
                new TransmissionAndAcceptanceBookCommand()
                { Id = id },
                CancellationToken.None);
            Books = await _mediator.Send(new GetBooksQuery());
            await GetSelectLists();
            _logger.LogInformation($"Book received");
        }
    }
}
