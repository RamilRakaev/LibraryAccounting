using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class LibrarianExcelReportCommand : IRequest<bool>
    {
        public string Path { get; set; }
    }
}
