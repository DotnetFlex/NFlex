using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex
{
    public static partial class Extensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> sendKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (sendKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source,FilterBuilder<TSource> filter)
        {
            return source.Where(filter.GetExpression());
        }
    }
}
