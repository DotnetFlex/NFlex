using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace NFlex.Caching.Memory
{
    public class MemoryCache:Cache
    {
        private System.Runtime.Caching.MemoryCache _memoryCache;

        public MemoryCache(string name):base()
        {
            _memoryCache = new System.Runtime.Caching.MemoryCache(name);
        }

        public override List<string> GetKeys(Func<string, bool> predicate = null)
        {
            var keys=_memoryCache.AsEnumerable().Select(t => t.Key);
            if (predicate == null)
                return keys.ToList();
            else
                return keys.Where(predicate).ToList();
        }

        protected override void SetCache(string key, object value, TimeSpan? expireTime = default(TimeSpan?))
        {
            var cachePolicy = new CacheItemPolicy();
            if (expireTime != null)
                cachePolicy.SlidingExpiration = expireTime.Value;
            _memoryCache.Set(key, value, cachePolicy);
        }

        protected override T GetCache<T>(string key) => _memoryCache.Get(key).To<T>();

        protected override void RemoveCache(string key)=> _memoryCache.Remove(key);

        public override void Dispose() => _memoryCache.Dispose();

        public override bool Contains(string key) => _memoryCache.Contains(key);
    }
}
