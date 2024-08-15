using System.Linq.Expressions;

namespace TaskManagement.Repository.Common
{
    public class SearchExpressionBuilder<T>
    {
        public Expression<Func<T, bool>> BuildOrSearchExpression(List<Expression<Func<T, bool>>> expressions)
        {
            if (expressions.Count == 1)
            {
                return expressions[0];
            }

            var orExpression = expressions.Skip(2).Aggregate(
                Expression.OrElse(expressions[0].Body, Expression.Invoke(expressions[1], expressions[0].Parameters[0])),
                (x, y) => Expression.OrElse(x, Expression.Invoke(y, expressions[0].Parameters[0])));

            return Expression.Lambda<Func<T, bool>>(orExpression, expressions[0].Parameters);
        }
    }
}
