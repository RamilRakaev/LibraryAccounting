using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods
{
    public class QueryFilter<Element, Query>
    {
        private readonly List<Element> _elements;
        public QueryFilter(List<Element> element)
        {
            _elements = element.ToList();
        }
        public List<Element> Filter(Query query)
        {
            var elementProperties = typeof(Element).GetProperties();
            var elementPropertyNames = elementProperties.Select(p => p.Name);

            foreach (var requestProperty in typeof(Query).GetProperties())
            {
                if (requestProperty.GetValue(query) != null)
                    if (elementPropertyNames.Contains(requestProperty.Name))
                    {
                        for (int i = 0; i < _elements.Count(); i++)
                        {
                            var elementProperty = elementProperties.FirstOrDefault(u => u.Name == requestProperty.Name);
                            if (elementProperty.GetValue(_elements[i]).ToString() != requestProperty.GetValue(query).ToString())
                            {
                                _elements.Remove(_elements[i]);
                            }
                        }
                    }
            }
            return _elements;
        }

        public async Task<List<Element>> FilterAsync(Query query)
        {
            return await Task.Run(() => Filter(query));
        }
    }
}
