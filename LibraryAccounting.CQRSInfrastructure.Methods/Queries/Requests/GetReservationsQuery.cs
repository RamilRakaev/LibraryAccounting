using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetReservationsQuery : IRequest<IEnumerable<Booking>>
    {
        public int CliendId { get; set; }
    }
}
