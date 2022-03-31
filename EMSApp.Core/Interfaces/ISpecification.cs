using System;
using System.Linq.Expressions;

namespace EMSApp.Core.Interfaces
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> ToExpression();
        bool IsSatisfiedBy(T entity);
    }
}
