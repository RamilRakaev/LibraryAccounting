using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetBooksQuery : IRequest<List<Book>>
    {
        public int? GenreId { get; set; } = null;
        public string Publisher { get; set; } = null;
        public int? AuthorId { get; set; } = null;
        public int? BookingId { get; set; } = null;
    }
}
