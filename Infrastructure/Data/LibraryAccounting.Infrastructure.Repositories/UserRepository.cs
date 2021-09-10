using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounting.Infrastructure.Repositories
{
    //public class UserRepository
    //{
    //    readonly private UserManager<ApplicationUser> db;

    //    public UserRepository(UserManager<ApplicationUser> context)
    //    {
    //        db = context;
    //    }

    //    public IEnumerable<ApplicationUser> GetAll()
    //    {
    //        return db.Users.Include(u => u.Role);
    //    }

    //    public ApplicationUser Find(int id)
    //    {
    //        return db.Users.FirstOrDefault(u => u.Id == id);
    //    }

    //    public void Add(ApplicationUser element)
    //    {
    //        db.CreateAsync(element);
    //    }

    //    public void Remove(ApplicationUser element)
    //    {
    //        if (db.Users.Contains(element))
    //            db.DeleteAsync(element);
    //    }

    //    public async Task<ApplicationUser> FindAsync(int id)
    //    {
    //        return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
    //    }

    //    public async Task AddAsync(ApplicationUser element)
    //    {
    //        await db.CreateAsync(element);
    //    }
    //}
}
