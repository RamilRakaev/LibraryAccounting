using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.AuthorMethods
{
    public class RemoveAuthorCommand : IRequest<Author>
    {
        public int Id { get; set; }

        public RemoveAuthorCommand(int id)
        {
            Id = id;
        }
    }

    public class RemoveAuthorHandler : IRequestHandler<RemoveAuthorCommand, Author>
    {
        private readonly IRepository<Author> _db;

        public RemoveAuthorHandler(IRepository<Author> db)
        {
            _db = db;
        }

        public async Task<Author> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            var Author = await _db.FindNoTrackingAsync(request.Id);
            await _db.RemoveAsync(Author);
            return Author;
        }
    }

    public class RemoveAuthorValidator : AbstractValidator<RemoveAuthorCommand>
    {
        public RemoveAuthorValidator()
        {
            RuleFor(g => g.Id).NotEqual(0);
        }
    }
}