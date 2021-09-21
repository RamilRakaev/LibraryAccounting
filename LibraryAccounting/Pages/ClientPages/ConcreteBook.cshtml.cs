using System;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.ClientPages
{
    public class ConcreteBookModel : PageModel
    {
        readonly private IMediator _mediator;
        readonly private ILogger<ConcreteBookModel> _logger;
        public new UserProperties User { get; private set; }
        public Book Book { get; private set; }

        public ConcreteBookModel(
            UserProperties userProperties, 
            IMediator mediator,
            ILogger<ConcreteBookModel> logger)
        {
            _mediator = mediator;
            User = userProperties;
            _logger = logger;
        }

        public async Task OnGet(int bookId)
        {
            _logger.LogInformation($"ConcreteBook page is visited");
            Book = await _mediator.Send(new GetBookQuery(bookId));
        }

        public async Task OnPost(int userId, int bookId)
        {
            await _mediator.Send(new AddBookingCommand(bookId, userId));
            _logger.LogInformation($"Added new booking");
            Book = await _mediator.Send(new GetBookQuery(bookId) { Id = bookId });
        }
    }
}
