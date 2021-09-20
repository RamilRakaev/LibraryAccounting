using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetBookQuery : IRequest<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public GetBookQuery(int id)
        {
            Id = id;
        }

        public GetBookQuery(string title)
        {
            Title = title;
        }
    }
}