using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetPublishersQuery : IRequest<string[]>
    {
    }
}
