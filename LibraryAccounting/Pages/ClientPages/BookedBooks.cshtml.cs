using System.Collections.Generic;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using MediatR;
using LibraryAccounting.CQRSInfrastructure.Methods.UserMethods;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;

namespace LibraryAccounting.Pages.ClientPages
{
    public class BookedBooksModel : PageModel
    {
        private readonly IMediator _mediator;
        public Dictionary<Booking, Book> armoredBooks { get; private set; }
        private readonly UserProperties _userProperties;

        public BookedBooksModel(UserProperties userProperties, IMediator mediator)
        {
            armoredBooks = new Dictionary<Booking, Book>();
            _userProperties = userProperties;
            _mediator = mediator;
            if (_userProperties.IsAuthenticated == false)
            {
                RedirectToPage("/Account/Login");
            }
        }

        public async Task OnGet()
        {
            armoredBooks = await _mediator.Send(new GetArmoredBooksQuery() { CliendId = _userProperties.UserId });
        }

        public async Task OnPost(int idBooking)
        {
            await _mediator.Send(new RemoveBookingCommand(idBooking));
            armoredBooks = await _mediator.Send(new GetArmoredBooksQuery() { CliendId = _userProperties.UserId });
        }
    }
}
