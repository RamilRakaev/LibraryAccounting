using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Handlers
{
    public class UserByEmailHandler : IReturningResultHandler<ApplicationUser, ApplicationUser>
    {
        readonly private string Email; 

        public UserByEmailHandler(string email)
        {
            Email = email;
        }

        public ApplicationUser Handle(IEnumerable<ApplicationUser> elements)
        {
            return elements.FirstOrDefault(u => u.Email == Email);
        }
    }

    public class UserLoginHandlerAsync : IReturningResultHandler<ApplicationUser, ApplicationUser>
    {
        readonly private string Email; 
        readonly private string Password; 

        public UserLoginHandlerAsync(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public  ApplicationUser Handle(IEnumerable<ApplicationUser> elements)
        {
            return elements.AsParallel().FirstOrDefault(u => u.Email == Email && u.Password == Password);
        }
    }
}
