using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core
{
    public interface IUpdateContext<TAggregateRoot>
    {
        void Set(Expression<Func<TAggregateRoot, TAggregateRoot>> updateExpression);
    }
}
