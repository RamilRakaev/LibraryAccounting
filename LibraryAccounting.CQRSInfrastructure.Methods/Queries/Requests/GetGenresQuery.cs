using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetGenresQuery : IRequest<IEnumerable<Genre>>
    {
        public string Name { get; set; }
    }
}
