using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Handlers
{
    public class UserByEmailHandler : IReturningResultHandler<User, User>
    {
        readonly private string Email; 

        public UserByEmailHandler(string email)
        {
            Email = email;
        }

        public User Handle(IEnumerable<User> elements)
        {
            return elements.FirstOrDefault(u => u.Email == Email);
        }
    }

    public class UserLoginHandlerAsync : IReturningResultHandler<User, User>
    {
        readonly private string Email; 
        readonly private string Password; 

        public UserLoginHandlerAsync(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public  User Handle(IEnumerable<User> elements)
        {
            return elements.AsParallel().FirstOrDefault(u => u.Email == Email && u.Password == Password);
        }
    }
}
