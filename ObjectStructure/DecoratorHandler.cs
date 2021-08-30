using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Handlers
{
    public class DecoratorHandler<Element> : IRequestsHandlerComponent<Element>
    {
        readonly private List<IRequestsHandlerComponent<Element>> HandlerComponents;

        public DecoratorHandler(List<IRequestsHandlerComponent<Element>> handlers)
        {
            HandlerComponents = handlers;
        }

        public DecoratorHandler(IRequestsHandlerComponent<Element> handler)
        {
            HandlerComponents = new List<IRequestsHandlerComponent<Element>>();
            HandlerComponents.Add(handler);
        }

        public void Handle(ref List<Element> elements)
        {
            foreach (var handler in HandlerComponents)
            {
                handler.Handle(ref elements);
            }
        }

        public Element ConcreteElement(List<Element> elements)
        {
            foreach (var handler in HandlerComponents)
            {
                handler.Handle(ref elements);
            }
            return elements.FirstOrDefault();
        }
    }
}
