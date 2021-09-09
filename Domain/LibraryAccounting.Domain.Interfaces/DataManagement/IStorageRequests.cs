using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    public interface IStorageRequests<Element>
    {
        public Element Find(int id);

        public Task<Element> FindAsync(int id)
        {
            throw new Exception("Method is not overridden in child classes");
        }

        public IEnumerable<Element> GetAll();
    }
}
