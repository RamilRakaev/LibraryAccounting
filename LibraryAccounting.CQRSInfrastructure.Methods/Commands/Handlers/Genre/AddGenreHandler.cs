using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class AddGenreHandler : IRequestHandler<AddGenreCommand, Genre>
    {
        private readonly IRepository<Genre> _db;

        public AddGenreHandler(IRepository<Genre> db)
        {
            _db = db;
        }
        public async Task<Genre> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre(request.Name);
            await _db.AddAsync(genre);
            await _db.SaveAsync();
            return genre;
        }
    }
}
