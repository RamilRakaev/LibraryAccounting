using System.Collections.Generic;

namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    
    public interface IRepository<Element> : IStorageRequests<Element>
    {
        public void Add(Element element);

        public void Remove(Element element);

        public void Save();
    }
}
