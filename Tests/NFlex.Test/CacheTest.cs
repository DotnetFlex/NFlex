using NFlex.Caching.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class CacheTest
    {
        [Fact]
        public void AutoReleaseLockTest()
        {
            RedisCache cache = new RedisCache("192.168.1.175:9006",1);
            string cacheKey = "Quantity1";
            using (var locker = cache.Lock(cacheKey))
            {
                int quantity=cache.Get<int>(cacheKey, () =>100);
                quantity -= 2;
                cache.Set(cacheKey, quantity);
            }
        }
    }
}
