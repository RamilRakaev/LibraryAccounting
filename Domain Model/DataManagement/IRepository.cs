using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IRepository<Element>
    {
        public IEnumerable<Element> GetAll();

        public void Add(Element element);

        public void Remove(Element element);

        public Element Find(int id);

        public void Save();
    }
}
