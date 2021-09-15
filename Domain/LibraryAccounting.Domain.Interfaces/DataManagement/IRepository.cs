using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    public interface IRepository<Element> : IStorageRequests<Element> where Element : IElement<Element>
    {
        public Task AddAsync(Element element);

        public Task RemoveAsync(Element element);

        public Task RemoveRangeAsync(IEnumerable<Element> elements)
        {
            throw new Exception("Method is not overridden in child class");
        }

        public Task SaveAsync();
    }
}
