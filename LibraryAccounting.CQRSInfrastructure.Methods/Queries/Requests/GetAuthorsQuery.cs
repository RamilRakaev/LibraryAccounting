using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetAuthorsQuery : IRequest<IEnumerable<BookAuthor>>
    {
        public string Name { get; set; }
    }
}
