using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class Filter
    {
        
        IQueryable<T> TextFilter<T>(IQueryable<T> source, string term)
        {
            if (string.IsNullOrEmpty(term)) { return source; }

            // T is a compile-time placeholder for the element type of the query.
            Type elementType = typeof(T);

            // Get all the string properties on this specific type.
            PropertyInfo[] stringProperties =
                elementType.GetProperties()
                    .Where(x => x.PropertyType == typeof(string))
                    .ToArray();
            if (!stringProperties.Any()) { return source; }

            // Get the right overload of String.Contains
            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            throw new Exception();
           
        }
    }
}
