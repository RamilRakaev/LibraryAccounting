﻿using Domain.Model;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        readonly private DataContext db;

        public UserRepository(DataContext context)
        {
            db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Find(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(User element)
        {
            db.Users.Add(element);
        }

        public void Remove(User element)
        {
            db.Users.Remove(element);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
