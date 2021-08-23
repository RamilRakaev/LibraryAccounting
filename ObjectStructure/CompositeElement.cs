using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.ObjectStructure
{
    public class CompositeElement<Element> : IElement<Element>
    {
        public int Id
        {
            get { return 0; }
            set { }
        }

        private IEnumerable<Element> Elements;

        public CompositeElement(IEnumerable<Element> elements)
        {
            Type type = elements.ElementAt(0).GetType();
            if (type.GetInterfaces().Contains(typeof(IElement<Element>)))
                Elements = elements;
            else
                throw new Exception("The elements does not implements the interface IElement");
        }

        public bool Accept(IVisitor<Element> visitor, IRequestsHandlerComponent<Element> handler)
        {

            bool successfulCompletion = true;
            if (handler != null)
            {
                handler.Handle(ref Elements);
            }
            handler.Handle(ref Elements);
            var elements = (IEnumerable<IElement>)Elements;
            var IElementVisisor = (IVisitor<IElement>)visitor;
            for (int i = 0; i < Elements.Count(); i++)
            {
                if (!elements.ElementAt(i).Accept(IElementVisisor))
                    successfulCompletion = false;
            }
            return successfulCompletion;
        }

        public bool Accept(IVisitor<Element> visitor)
        {
            return Accept(visitor, null);
        }
    }
}
