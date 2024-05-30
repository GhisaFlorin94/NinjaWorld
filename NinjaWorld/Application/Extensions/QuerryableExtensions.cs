using NinjaWorld.Application.Models.Orders;
using System.Linq.Expressions;

namespace NinjaWorld.Application.Extensions
{
    public static class QuerryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByProperty, OrderDirection orderDirection)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, orderByProperty);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = orderDirection == OrderDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var method = typeof(Queryable).GetMethods()
                .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                .Single()
                .MakeGenericMethod(typeof(T), property.Type);

            return (IQueryable<T>)method.Invoke(null, new object[] { source, lambda });
        }
    }
}
