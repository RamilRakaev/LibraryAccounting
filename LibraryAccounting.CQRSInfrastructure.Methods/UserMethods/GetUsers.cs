using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class GetUsersCommand : IRequest<List<User>>
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int? RoleId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class GetUsersHandler : UserHandler, IRequestHandler<GetUsersCommand, List<User>>
    {
        public GetUsersHandler(IRepository<User> _db) : base(_db)
        { }

        private List<User> Users;
        async Task<List<User>> IRequestHandler<GetUsersCommand, List<User>>.Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            Users = db.GetAll().ToList();
            if (request.Email != null && request.Password != null)
                return await Task.Run(() => db.GetAll().Where(u => u.Name == request.Email && u.Password == request.Password).ToList());
            else
                return await Task.Run(() => Users);
        }

        private List<User> Filter(string[] args)
        {
            if (args.Length % 2 == 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    Filter(args[i], args[i + 1]);
                }
            }
            throw new ArgumentException();
        }

        private void Filter(string property, string value)
        {
            for (int i = 0; i < Users.Count(); i++)
            {
                PropertyInfo propertyInfo = typeof(User).GetProperty(property);
                if (value != propertyInfo.GetValue(Users[i]).ToString())
                {
                    Users.Remove(Users[i]);
                }
            }
        }
    }

}
