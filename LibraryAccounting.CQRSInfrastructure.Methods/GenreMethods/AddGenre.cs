using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.GenreMethods
{
    public class AddGenreCommand : IRequest<Genre>
    {
        public string Name { get; set; }

        public AddGenreCommand(string name)
        {
            Name = name;
        }
    }

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

    public class GenreValidator : AbstractValidator<AddGenreCommand>
    {
        public GenreValidator()
        {
            RuleFor(g => g.Name).NotNull().NotEmpty();
        }
    }
}
