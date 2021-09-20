using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries
{
    public class QueryFilter<Element, Query>
    {
        private List<Element> _elements;
        public QueryFilter(List<Element> element)
        {
            _elements = element.ToList();
        }
        public List<Element> Filter(Query query)
        {
            var queryProperties = typeof(Query).GetProperties().Where(p => p.GetValue(query) != null 
            && p.GetValue(query).ToString() != "0" 
            && p.GetValue(query).ToString() != string.Empty);
            var elementProperties = typeof(Element)
                .GetProperties()
                .Where(g => queryProperties
                .Select(p => p.Name)
                .Contains(g.Name));

            foreach (var elementProperty in elementProperties)
            {
                int i = 0;
                while (i < _elements.Count())
                {
                    bool success = true;
                    var a = elementProperty
                        .GetValue(_elements[i])
                        .ToString();

                    var b = queryProperties
                        .FirstOrDefault(q => q.Name == elementProperty.Name)
                        .GetValue(query).ToString();
                    if (a != b)
                    {
                        _elements.Remove(_elements[i]);
                        success = false;
                    }

                    if (success)
                        i++;
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
