using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class DeletingExpiredBooksCommand : IRequest<IEnumerable<Booking>>
    {
        public int MaxBookingPeriod { get; set; } = 3;
    }
}
