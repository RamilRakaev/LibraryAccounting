using System;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.BookMethods;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.ClientPages
{
    public class ConcreteBookModel : PageModel
    {
        readonly private IClientTools _clientTools;
        readonly private IMediator _mediator;
        public UserProperties UserProperties { get; private set; }
        public Book Book { get; private set; }
        public bool IsFree { get; private set; }

        public ConcreteBookModel(IClientTools clientTools, UserProperties userProperties, IMediator mediator)
        {
            _clientTools = clientTools;
            _mediator = mediator;
            UserProperties = userProperties;
        }

        public async Task OnGet(int id, bool isBooking)
        {
            Book = await Task.Run(() => ExtractBook(_clientTools, id));
            IsFree = isBooking;
        }

        public async Task OnPost(int userId, int bookId)
        {
            await _mediator.Send(new AddBookingCommand() { ClientId = userId, BookId = bookId });
            Book = await _mediator.Send(new GetBookCommand() { Id = bookId });
            IsFree = false;
        }

        private static Book ExtractBook(IClientTools clientTools, int bookId)
        {
            return clientTools.GetBook(bookId);
        }
    }
}
