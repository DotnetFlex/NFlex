using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Caching
{
    public abstract class CacheBase : ICache
    {
        private readonly Dictionary<string, object> _lockHelper;

        public string Name { get; set; }

        protected CacheBase(string name)
        {
            Name = name;
        }

        public void Set(string key, object target,TimeSpan? expireTime = default(TimeSpan?))
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            if (target == null) return;
            SetCache(key.Trim(), target, expireTime);
        }

        public void RemoveAll()
        {
            var keys = GetKeys();
            foreach (var key in keys)
                Remove(key);
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            return GetCache<T>(key.Trim());
        }
        

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            RemoveCache(key.Trim());
        }



        public void Remove(Func<string,bool> predicate)
        {
            var list = new List<ICache>();
            var keys = GetKeys().Where(predicate).ToList();
            foreach (string key in keys)
            {
                Remove(key);
            }
        }

        public abstract bool ContainsKey(string key);
        protected abstract T GetCache<T>(string key);
        protected abstract void RemoveCache(string key);
        protected abstract void SetCache(string key, object value, TimeSpan? expireTime = null);
        public abstract List<string> GetKeys(Func<string, bool> predicate = null);
        public virtual void Dispose() { }

    }
}
