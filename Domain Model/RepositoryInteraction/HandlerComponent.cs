using System.Collections.Generic;

namespace Domain.Interfaces
{
    public abstract class HandlerComponent<Element>
    {
        protected IEnumerable<Element> Elements { get; set; }

        public HandlerComponent()
        { }

        public bool SetElements(IEnumerable<Element> elements)
        {
            if (Elements == null)
            {
                Elements = elements;
                return true;
            }
            return false;
        }
        public abstract IEnumerable<Element> Handle();
    }
}
