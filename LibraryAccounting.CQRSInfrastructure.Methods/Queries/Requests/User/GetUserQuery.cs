using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetUserQuery : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
    }
}
