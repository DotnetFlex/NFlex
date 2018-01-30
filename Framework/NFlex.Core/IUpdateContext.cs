using System;
using System.Linq.Expressions;

namespace NFlex.Core
{
    public interface IUpdateContext<TAggregateRoot>
    {
        void Set(Expression<Func<TAggregateRoot, TAggregateRoot>> updateExpression);
    }
}
