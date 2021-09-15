using System;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.BookMethods;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.ClientPages
{
    public class ConcreteBookModel : PageModel
    {
        readonly private IClientTools _clientTools;
        readonly private IMediator _mediator;
        readonly private ILogger<ConcreteBookModel> _logger;
        public UserProperties UserProperties { get; private set; }
        public Book Book { get; private set; }
        public bool IsFree { get; private set; }

        public ConcreteBookModel(IClientTools clientTools, 
            UserProperties userProperties, 
            IMediator mediator,
            ILogger<ConcreteBookModel> logger)
        {
            _clientTools = clientTools;
            _mediator = mediator;
            UserProperties = userProperties;
            _logger = logger;
        }

        public async Task OnGet(int bookId, bool isBooking)
        {
            _logger.LogInformation($"ConcreteBook page is visited: {DateTime.Now:T}");
            Book = await Task.Run(() => ExtractBook(_clientTools, bookId));
            Book = await _mediator.Send(new GetBookCommand(bookId));
            IsFree = isBooking;
        }

        public async Task OnPost(int userId, int bookId)
        {
            await _mediator.Send(new AddBookingCommand() { ClientId = userId, BookId = bookId });
            _logger.LogInformation($"Added new booking: {DateTime.Now:T}");
            Book = await _mediator.Send(new GetBookCommand(bookId) { Id = bookId });
            IsFree = false;
        }

        private Book ExtractBook(IClientTools clientTools, int bookId)
        {
            _logger.LogInformation($"Extracting a book from the database: {DateTime.Now:T}");
            return clientTools.GetBook(bookId);
        }
    }
}
