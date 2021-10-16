using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Parser;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Domain.Services.Core
{


    public static class IEnumerableExtensions
    {
        public static async Task<IOrderedEnumerable<T>> OrderByCustom<T>(this IEnumerable<T> source, string propertyName, IComparer<string> comparer, string direction) 
        {

                ParameterExpression paramExp = Expression.Parameter(typeof(T), "x");

                Expression propExp = Expression.Property(paramExp, propertyName);

                var resultExp = Expression.Lambda(propExp, paramExp);

                var lambda = resultExp.Compile();

                var methods = typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static);

                var methodName = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";

                var method = methods.Where(i => i.Name == methodName && i.GetParameters().Count() == 3).FirstOrDefault();

                if (method == null)                
                    throw new NullReferenceException(string.Format("method {0} not found", methodName));
                

                

                method = method.MakeGenericMethod(typeof(T), propExp.Type);

                var result = (IOrderedEnumerable<T>)method.Invoke(null, new object[] { source, lambda, comparer });

                return result;
           

        }




    }
}
