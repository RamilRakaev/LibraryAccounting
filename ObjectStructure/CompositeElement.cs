using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.ObjectStructure
{
    public class CompositeElement : IElement
    {
        public int Id
        {
            get { return 0; }
            set { }
        }

        readonly private IRepository<IElement> Repository;
        private IEnumerable<IElement> Elements;

        public CompositeElement(IRepository<IElement> repository)
        {
            Repository = repository;
            Elements = Repository.GetAll();
        }

        public void Accept(IVisitor<IElement> visitor)
        {
            for (int i = 0; i < Elements.Count(); i++)
            {
                Elements.ElementAt(i).Accept(visitor);
                Repository.Save();
            }
        }

        public void ConcreteElementAccept(IVisitor<IElement> visitor, int id)
        {
            Repository.Find(id).Accept(visitor);
            Repository.Save();
        }
    }
}
