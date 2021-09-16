using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.AuthorMethods
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public string Name { get; set; }

        public AddAuthorCommand(string name)
        {
            Name = name;
        }
    }

    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IRepository<Author> _db;
        public AddAuthorHandler(IRepository<Author> db)
        {
            _db = db;
        }
        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author(request.Name);
            await _db.AddAsync(author);
            await _db.SaveAsync();
            return author;
        }
    }

    public class AuthorValidator : AbstractValidator<AddAuthorCommand>
    {
        public AuthorValidator()
        {
            RuleFor(g => g.Name).NotNull().NotEmpty();
        }
    }
}
