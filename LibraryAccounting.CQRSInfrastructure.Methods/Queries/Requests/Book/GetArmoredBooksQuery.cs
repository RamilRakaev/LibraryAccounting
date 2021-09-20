using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetArmoredBooksQuery : IRequest<List<Book>>
    {
        public int ClientId { get; set; }

        public GetArmoredBooksQuery()
        { }

        public GetArmoredBooksQuery(int clientId)
        {
            ClientId = clientId;
        }
    }
}
