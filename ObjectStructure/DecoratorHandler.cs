using Domain.Interfaces;
using System.Collections.Generic;

namespace Infrastructure.ObjectStructure
{
    public class DecoratorHandler<Element> : HandlerComponent<Element>
    {
        readonly private IEnumerable<HandlerComponent<Element>> HandlerComponents;

        public DecoratorHandler(IEnumerable<Element> elements)
        {
            Elements = elements;
        }

        public DecoratorHandler(IEnumerable<HandlerComponent<Element>> handlers, IEnumerable<Element> elements)
        {
            HandlerComponents = handlers;
            Elements = elements;
        }

        public override IEnumerable<Element> Handle()
        {
            if (HandlerComponents != null)
            {
                foreach (var handler in HandlerComponents)
                {
                    handler.SetElements(Elements);
                    Elements = handler.Handle();
                }
            }
            return Elements;
        }
    }
}
