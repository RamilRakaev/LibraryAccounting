using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUserManager> store) : base(store)
        { }
    }
    public class UserRepository : IRepository<ApplicationUser>
    {
        readonly private DataContext db;

        public UserRepository(DataContext context)
        {
            db = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users.Include(u => u.Role);
        }

        public ApplicationUser Find(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(ApplicationUser element)
        {
            db.Users.Add(element);
        }

        public void Remove(ApplicationUser element)
        {
            if (db.Users.Contains(element))
                db.Users.Remove(element);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task<ApplicationUser> FindAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task AddAsync(ApplicationUser element)
        {
            await db.Users.AddAsync(element);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
