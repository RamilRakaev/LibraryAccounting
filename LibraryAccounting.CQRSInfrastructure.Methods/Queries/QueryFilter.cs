using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries
{
    public class QueryFilter<Element, Query>
    {
        private List<Element> _elements;
        public QueryFilter(IQueryable<Element> element)
        {
            _elements = element.ToList();
        }
        public List<Element> Filter(Query query)
        {
            var queryProperties = typeof(Query)
                .GetProperties()
                .Where(p =>
                p.GetValue(query) != null &&
                p.GetValue(query).ToString() != "0" &&
                p.GetValue(query).ToString() != string.Empty);

            var parameterExp = Expression.Parameter(typeof(PropertyInfo), "x");
            var propertyExp = Expression.Property(parameterExp, "Name");
            var resultExp = Expression.Lambda(propertyExp, parameterExp);

            var selectLambda = resultExp.Compile();

            Type enumerableType = typeof(Enumerable);
            var methods = enumerableType
                .GetMethods(BindingFlags.Public | BindingFlags.Static);

            var selectMethod = methods.FirstOrDefault(m => m.Name == "Select" && m.GetParameters().Count() == 2);
            selectMethod = selectMethod.MakeGenericMethod(typeof(PropertyInfo), propertyExp.Type);

            var queryPropertyNames = (IEnumerable<string>)selectMethod.Invoke(null,
                new object[] { queryProperties, selectLambda });

            var elementProperties = typeof(Element)
                .GetProperties().AsEnumerable();
            Expression<Func<PropertyInfo, bool>> whereExpLambda = g => queryPropertyNames.Contains(g.Name);

            var y = Expression.Parameter(typeof(PropertyInfo), "y");

            var whereMethod = methods.FirstOrDefault(m => m.Name == "Where");
            whereMethod = whereMethod.MakeGenericMethod(typeof(PropertyInfo));

            elementProperties = (IEnumerable<PropertyInfo>)whereMethod
                .Invoke(typeof(IEnumerable<PropertyInfo>),
                new object[] { elementProperties, whereExpLambda.Compile() });

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
