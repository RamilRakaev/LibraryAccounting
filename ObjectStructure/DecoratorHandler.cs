using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Handlers
{
    public class DecoratorHandler<Element> : IRequestsHandlerComponent<Element>
    {
        readonly private List<IRequestsHandlerComponent<Element>> HandlerComponents;

        public DecoratorHandler()
        {
            HandlerComponents = new List<IRequestsHandlerComponent<Element>>();
        }

        public DecoratorHandler(List<IRequestsHandlerComponent<Element>> requestsHandlers)
        {
            HandlerComponents = requestsHandlers;
        }

        public void Add(IRequestsHandlerComponent<Element> requestsHandler)
        {
            if(requestsHandler != null)
            HandlerComponents.Add(requestsHandler);
        }

        public void Remove(IRequestsHandlerComponent<Element> requestsHandler)
        {
            if (requestsHandler != null)
                HandlerComponents.Remove(requestsHandler);
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
