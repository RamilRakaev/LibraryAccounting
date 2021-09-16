using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.GenreMethods
{
    public class RemoveGenreCommand : IRequest<Genre>
    {
        public int Id { get; set; }

        public RemoveGenreCommand(int id)
        {
            Id = id;
        }
    }

    public class RemoveGenreHandler : IRequestHandler<RemoveGenreCommand, Genre>
    {
        private readonly IRepository<Genre> _db;

        public RemoveGenreHandler(IRepository<Genre> db)
        {
            _db = db;
        }

        public async Task<Genre> Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _db.FindAsync(request.Id);
            await _db.RemoveAsync(genre);
            return genre;
        }
    }

    public class RemoveGenreValidator : AbstractValidator<RemoveGenreCommand>
    {
        public RemoveGenreValidator()
        {
            RuleFor(g => g.Id).NotEqual(0);
        }
    }
}