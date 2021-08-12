using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DomainRepository
{
    public abstract class AbstractVisitor<Element>
    {
        readonly AbstractRepository<Element> Repository;
        public AbstractVisitor(AbstractRepository<Element> repository)
        {
            Repository = repository;
        }

        public bool Visit(int id)
        {
            Element element = Repository.Find(id);
            if (element != null)
            {
                Operation();
                return true;
            }
            else
                return false;
        }

        abstract protected void Operation();
    }
}
