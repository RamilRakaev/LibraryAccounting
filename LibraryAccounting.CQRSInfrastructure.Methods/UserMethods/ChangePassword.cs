using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class ChangePasswordCommand : IRequest<User>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, User>
    {
        private readonly IRepository<User> db;

        public ChangePasswordHandler(IRepository<User> _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(IRepository<User>));
        }

        public async Task<User> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await db.FindAsync(request.Id);
            if (user != null)
            {
                user.Password = request.Password;
                await db.SaveAsync();
                return user;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
