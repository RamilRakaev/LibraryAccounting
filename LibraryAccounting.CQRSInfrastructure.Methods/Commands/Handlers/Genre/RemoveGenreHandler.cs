using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class RemoveGenreHandler : IRequestHandler<RemoveGenreCommand, Genre>
    {
        private readonly IRepository<Genre> _db;

        public RemoveGenreHandler(IRepository<Genre> db)
        {
            _db = db;
        }

        public async Task<Genre> Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _db.FindNoTrackingAsync(request.Id);
            await _db.RemoveAsync(genre);
            return genre;
        }
    }
}
