using Domain.Model;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IRepository<PersonAccount>
    {
        readonly private DataContext db;

        public PersonRepository(DataContext context)
        {
            db = context;
        }

        public IEnumerable<PersonAccount> GetAll()
        {
            return db.Users;
        }

        public PersonAccount Find(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(PersonAccount element)
        {
            db.Users.Add(element);
        }

        public void Remove(PersonAccount element)
        {
            db.Users.Remove(element);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
