using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YoAyudoPR.Web.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);

            PropertyInfo propertyInfo = entityType.GetProperty(propertyName) ?? throw new ArgumentException($"Property {propertyName} not found in type {entityType}");
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            LambdaExpression selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            var enumerableType = typeof(Queryable);
            var method = enumerableType.GetMethods()
                .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
                .Where(m =>
                {
                    var parameters = m.GetParameters().ToList();
                    return parameters.Count == 2;
                }).Single();

            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            var newQuery = (IOrderedQueryable<TSource>?)genericMethod
                .Invoke(genericMethod, new object[] { query, selector });

            return newQuery!;
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
            this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);

            PropertyInfo propertyInfo = entityType.GetProperty(propertyName) ?? throw new ArgumentException($"Property {propertyName} not found in type {entityType}");
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            var enumerableType = typeof(System.Linq.Queryable);
            var method = enumerableType.GetMethods()
                .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition)
                .Where(m =>
                {
                    var parameters = m.GetParameters().ToList();
                    return parameters.Count == 2;
                }).Single();

            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                .Invoke(genericMethod, new object[] { query, selector });

            return newQuery;
        }
    }
}
