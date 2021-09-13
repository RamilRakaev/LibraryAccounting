using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Handlers
{
    public class UserByEmailHandler : IReturningResultHandler<ApplicationUser, ApplicationUser>
    {
        readonly private string _email; 

        public UserByEmailHandler(string email)
        {
            _email = email;
        }

        public ApplicationUser Handle(IEnumerable<ApplicationUser> elements)
        {
            return elements.FirstOrDefault(u => u.Email == _email);
        }
    }

    public class UserLoginHandlerAsync : IReturningResultHandler<ApplicationUser, ApplicationUser>
    {
        readonly private string _email; 
        readonly private string _password; 

        public UserLoginHandlerAsync(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public  ApplicationUser Handle(IEnumerable<ApplicationUser> elements)
        {
            return elements.AsParallel().FirstOrDefault(u => u.Email == _email && u.Password == _password);
        }
    }
}
