using System.Collections.Generic;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using MediatR;
using LibraryAccounting.CQRSInfrastructure.Methods.UserMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;
using Microsoft.Extensions.Logging;
using System;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookedBooksModel : PageModel
    {
        private readonly IMediator _mediator;
        public Dictionary<Booking, Book> ArmoredBooks { get; private set; }
        private readonly UserProperties _userProperties;
        private readonly ILogger<BookedBooksModel> _logger;

        public BookedBooksModel(UserProperties userProperties, 
            IMediator mediator,
            ILogger<BookedBooksModel> logger)
        {
            ArmoredBooks = new Dictionary<Booking, Book>();
            _userProperties = userProperties;
            _mediator = mediator;
            _logger = logger;
            if (_userProperties.IsAuthenticated == false)
            {
                RedirectToPage("/Account/Login");
            }
        }

        public async Task OnGet()
        {
            _logger.LogInformation($"BookedBooks page is visited: {DateTime.Now:T}");
               ArmoredBooks = await _mediator.Send(new GetArmoredBooksQuery() { CliendId = _userProperties.UserId });
        }

        public async Task OnPost(int idBooking)
        {
            await _mediator.Send(new RemoveBookingCommand(idBooking));
            _logger.LogDebug($"Booking {idBooking} is removed: {DateTime.Now:T}");
            ArmoredBooks = await _mediator.Send(new GetArmoredBooksQuery() { CliendId = _userProperties.UserId });
        }
    }
}
