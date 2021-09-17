using System.Collections.Generic;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using MediatR;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;
using Microsoft.Extensions.Logging;
using System;
using LibraryAccounting.CQRSInfrastructure.Methods.BookMethods;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookedBooksModel : PageModel
    {
        private readonly IMediator _mediator;
        public List<Book> ArmoredBooks { get; private set; }
        private readonly new UserProperties User;
        private readonly ILogger<BookedBooksModel> _logger;

        public BookedBooksModel(UserProperties user,
            IMediator mediator,
            ILogger<BookedBooksModel> logger)
        {
            User = user;
            _mediator = mediator;
            _logger = logger;
            if (User.IsAuthenticated == false)
            {
                RedirectToPage("/Account/Login");
            }
        }

        public async Task OnGet()
        {
            _logger.LogInformation($"BookedBooks page is visited: {DateTime.Now:T}");
            ArmoredBooks = await _mediator.Send(new GetArmoredBooksQuery(User.UserId));

        }

        public async Task OnPost(int idBooking)
        {
            await _mediator.Send(new RemoveBookingCommand(idBooking));
            _logger.LogDebug($"Booking {idBooking} is removed: {DateTime.Now:T}");
            ArmoredBooks = await _mediator.Send(new GetArmoredBooksQuery(User.UserId));
        }
    }
}
