using System;
using System.Linq.Expressions;

namespace NFlex
{
    public class FilterBuilder<TEntity>
    {
        private readonly ParameterExpression _parameter;
        private Expression<Func<TEntity,bool>> _result;
        public FilterBuilder()
        {
            _parameter = Expression.Parameter(typeof(TEntity), "t");
        }
        public FilterBuilder(Expression<Func<TEntity, bool>> expr):base()
        {
            _result = expr;
        }

        public Expression<Func<TEntity, bool>> GetExpression()
        {
            if (_result == null)
                _result = t => true;
            return _result;
        }

        public Func<TEntity,bool> Compile()
        {
            if (_result == null)
                _result = t => true;
            return _result.Compile();
        }


        public FilterBuilder<TEntity> And(Expression<Func<TEntity,bool>> expr)
        {
            if(_result==null)
            {
                _result = expr;
                return this;
            }
            // 首先定义好一个ParameterExpression
            var candidateExpr = Expression.Parameter(typeof(TEntity), "t");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            // 将表达式树的参数统一替换成我们定义好的candidateExpr
            var left = parameterReplacer.Replace(_result.Body);
            var right = parameterReplacer.Replace(expr.Body);

            var body = Expression.And(left, right);

            _result= Expression.Lambda<Func<TEntity, bool>>(body, candidateExpr);
            return this;
        }

        public FilterBuilder<TEntity> And(FilterBuilder<TEntity> filter)
        {
            And(filter.GetExpression());
            return this;
        }

        public FilterBuilder<TEntity> Or(Expression<Func<TEntity, bool>> expr)
        {
            if (_result == null)
            {
                _result = expr;
                return this;
            }

            var candidateExpr = Expression.Parameter(typeof(TEntity), "t");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(_result.Body);
            var right = parameterReplacer.Replace(expr.Body);
            var body = Expression.Or(left, right);

            _result= Expression.Lambda<Func<TEntity, bool>>(body, candidateExpr);
            return this;
        }

        public FilterBuilder<TEntity> Or(FilterBuilder<TEntity> filter)
        {
            Or(filter.GetExpression());
            return this;
        }
    }
}
