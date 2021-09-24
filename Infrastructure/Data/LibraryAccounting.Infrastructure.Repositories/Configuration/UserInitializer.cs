using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories.Configuration
{
    public class UserInitializer
    {
        private static List<ApplicationUser> users = new List<ApplicationUser>()
        {
            new ApplicationUser(){ UserName = "Danil", Email = "Danil@gmail.com",
                    RoleId = 3, EmailConfirmed = true},
            new ApplicationUser(){ UserName = "Ivan", Email = "Ivan@gmail.com",
                RoleId = 2, EmailConfirmed = true},
            new ApplicationUser(){ UserName = "Denis", Email = "Denis@gmail.com",
                    RoleId = 1, EmailConfirmed = true},
        };

        private static List<string> passwords = new List<string>() 
        {
            "e23D!23df32", "sdfgDs23de#", "Fd3D23d3&2r4"
        };

        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager)
        {
            int i = 0;
            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    await userManager.CreateAsync(user, passwords[i++]);
                }
            }
        }
    }
}
