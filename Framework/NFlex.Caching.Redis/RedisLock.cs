using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Caching.Redis
{
    public class RedisLock : IDisposable
    {
        private string _key;
        private RedisCache _cache;

        public RedisLock(string key,RedisCache cache)
        {
            _key = key;
            _cache = cache;
        }

        public void Dispose()
        {
            _cache.UnLock(_key);
        }
    }
}
