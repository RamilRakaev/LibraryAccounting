using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Handlers
{
    public class DecoratorHandler<Element> : IRequestsHandlerComponent<Element>
    {
        readonly private List<IRequestsHandlerComponent<Element>> handlerComponents;

        public DecoratorHandler()
        {
            handlerComponents = new List<IRequestsHandlerComponent<Element>>();
        }

        public DecoratorHandler(IRequestsHandlerComponent<Element> handler)
        {
            handlerComponents = new List<IRequestsHandlerComponent<Element>>();
            handlerComponents.Add(handler);
        }

        public DecoratorHandler(List<IRequestsHandlerComponent<Element>> requestsHandlers)
        {
            handlerComponents = requestsHandlers;
        }

        public void Add(IRequestsHandlerComponent<Element> requestsHandler)
        {
            if(requestsHandler != null)
            handlerComponents.Add(requestsHandler);
        }

        public void Remove(IRequestsHandlerComponent<Element> requestsHandler)
        {
            if (requestsHandler != null)
                handlerComponents.Remove(requestsHandler);
        }

        public void Handle(ref List<Element> elements)
        {
            foreach (var handler in handlerComponents)
            {
                handler.Handle(ref elements);
            }
        }

        public Element ConcreteElement(List<Element> elements)
        {
            foreach (var handler in handlerComponents)
            {
                handler.Handle(ref elements);
            }
            return elements.FirstOrDefault();
        }
    }
}
