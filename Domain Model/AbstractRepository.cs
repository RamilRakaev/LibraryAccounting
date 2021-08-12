using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DomainRepository
{
    public interface AbstractRepository<Element>
    {
        Element Find(int id);

        void Save();
    }
}
