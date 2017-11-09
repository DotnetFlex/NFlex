using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Caching
{
    public class CacheManager<TCacheProvider> :ICacheManager<TCacheProvider> where TCacheProvider:ICacheProvider
    {
        private ICache _cache;
        private readonly Dictionary<string, object> _lockHelper;
        public CacheManager(TCacheProvider provider)
        {
            _cache = provider.Instance();
            _lockHelper = new Dictionary<string, object>();
        }

        /// <summary>
        /// 检查指定的缓存键是否存在
        /// </summary>
        public bool Exists(string key) => _cache.ContainsKey(key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireTime">缓存有效期</param>
        public void Set(string key, object value, TimeSpan? expireTime = default(TimeSpan?))
        {
            _cache.Set(key, value, expireTime);
        }

        /// <summary>
        /// 根据键获取缓存内容
        /// </summary>
        /// <typeparam name="T">缓存值类型</typeparam>
        /// <param name="key">缓存键</param>
        public T Get<T>(string key) => _cache.Get<T>(key);

        /// <summary>
        /// 根据键获取缓存内容
        /// </summary>
        /// <typeparam name="T">缓存内容类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="addHandler">未获取到缓存内容时要执行的方法</param>
        /// <param name="expireTime">缓存有效期</param>
        /// <returns></returns>
        public virtual T Get<T>(string key, Func<T> addHandler, TimeSpan? expireTime = default(TimeSpan?))
        {
            T result;
            if (_cache.ContainsKey(key))
            {
                result = _cache.Get<T>(key);
                if (result != null) return result;
            }

            if (!_lockHelper.ContainsKey(key))
                _lockHelper.Add(key, new object());
            var lockObj = _lockHelper[key];

            lock (lockObj)
            {
                if (_cache.ContainsKey(key))
                {
                    result = _cache.Get<T>(key);
                    if (result != null) return result;
                }

                result = addHandler();
                if (result != null) _cache.Set(key, result, expireTime);
            }
            return result;
        }

        /// <summary>
        /// 获取所有键名
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        public List<string> GetKeys(Func<string, bool> predicate=null) => _cache.GetKeys(predicate);

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key) => _cache.Remove(key);

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="predicate"></param>
        public void Remove(Func<string, bool> predicate)=> _cache.Remove(predicate);
        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public void RemoveAll() => _cache.RemoveAll();
    }
}
