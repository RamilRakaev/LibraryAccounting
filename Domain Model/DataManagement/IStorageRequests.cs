using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    public interface IStorageRequests<Element>
    {
        public Element Find(int id);

        public IEnumerable<Element> GetAll();
    }
}
