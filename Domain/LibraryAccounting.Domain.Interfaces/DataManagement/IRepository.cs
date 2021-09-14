using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    
    public interface IRepository<Element> : IStorageRequests<Element>
    {
        public void Add(Element element);

        public void Remove(Element element);

        public void RemoveRange(IEnumerable<Element> elements) 
        {
            throw new Exception("Method is not overridden in child class");
        }

        public void Save();

        public Task AddAsync(Element element)
        {
            throw new Exception();
        }
        public Task SaveAsync()
        {
            throw new Exception();
        }
    }
}
