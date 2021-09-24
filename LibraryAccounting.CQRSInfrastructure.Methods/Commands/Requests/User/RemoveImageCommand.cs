using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class RemoveImageCommand : IRequest<string>
    {
        public string Title { get; set; }

        public RemoveImageCommand(string title)
        {
            Title = title;
        }
    }
}
