using DynamicQuery.Constants;
using DynamicQuery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DynamicQuery.Expressions
{
    public static class DynamicExpression
    {
        public static IQueryable<TModel> DynamicFilter<TModel>(this IQueryable<TModel> iqueryable, IEnumerable<DynamicModel> dynamicModel)
        {
            return iqueryable.Where(Filter<TModel>(dynamicModel));
        }
        public static Expression<Func<TModel, bool>> Filter<TModel>(IEnumerable<DynamicModel> dynamicModel)
        {
            Expression<Func<TModel, bool>> result = a => true;
            foreach (var item in dynamicModel)
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TModel));
                MemberExpression memberExpression = Expression.Property(parameterExpression, item.Name);
                ConstantExpression constantExpression = Expression.Constant(item.Data);
                BinaryExpression comparison = GetBinaryExpression(item.Comparison, memberExpression, constantExpression);
                var expression1 = Expression.Lambda<Func<TModel, bool>>(comparison, parameterExpression);
                var param = Expression.Parameter(typeof(TModel), "x");
                var body = Expression.AndAlso(
                            Expression.Invoke(result, param),
                            Expression.Invoke(expression1, param)
                        );
                result = Expression.Lambda<Func<TModel, bool>>(body, param);
            }
            return result;
        }
        private static BinaryExpression GetBinaryExpression(string comparison, MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            switch (comparison)
            {
                case ComparisonConstant.Equal:
                    return Expression.Equal(memberExpression, constantExpression);
                case ComparisonConstant.LessThan:
                    return Expression.LessThan(memberExpression, constantExpression);
                case ComparisonConstant.GreaterThan:
                    return Expression.GreaterThan(memberExpression, constantExpression);
                case ComparisonConstant.NotEqual:
                    return Expression.NotEqual(memberExpression, constantExpression);
                case ComparisonConstant.GreaterThanEqual:
                    return Expression.GreaterThanOrEqual(memberExpression, constantExpression);
                case ComparisonConstant.LessThanEqual:
                    return Expression.LessThanOrEqual(memberExpression, constantExpression);
                default:
                    return null;
            }
        }
    }
}
