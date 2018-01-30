using NFlex.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlex.Caching
{
    public abstract class Cache:IDisposable, ISingletonDependency
    {
        private readonly Dictionary<string, object> _lockHelper=new Dictionary<string, object>();
        protected readonly string _cacheLockKeys = "CacheLockKeys";

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">缓存对象</param>
        /// <param name="expireTime">缓存有效期</param>
        public void Set(string key, object value, TimeSpan? expireTime = default(TimeSpan?))
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            if (value == null) return;
            SetCache(key.Trim(), value, expireTime);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存内容类型</typeparam>
        /// <param name="key">键名</param>
        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            return GetCache<T>(key.Trim());
        }

        /// <summary>
        /// 根据键获取缓存内容
        /// </summary>
        /// <typeparam name="T">缓存内容类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="addHandler">未获取到缓存内容时要执行的方法</param>
        /// <param name="expireTime">缓存有效期</param>
        public virtual T Get<T>(string key, Func<T> addHandler, TimeSpan? expireTime = default(TimeSpan?))
        {
            T result;
            if (Contains(key))
            {
                result = Get<T>(key);
                if (result != null) return result;
            }

            if (!_lockHelper.ContainsKey(key))
                _lockHelper.Add(key, new object());
            var lockObj = _lockHelper[key];

            lock (lockObj)
            {
                if (Contains(key))
                {
                    result = Get<T>(key);
                    if (result != null) return result;
                }

                result = addHandler();
                if (result != null) Set(key, result, expireTime);
            }
            return result;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">键名</param>
        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            RemoveCache(key.Trim());
        }

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public void RemoveAll()
        {
            var keys = GetKeys();
            foreach (var key in keys)
                Remove(key);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        public void Remove(Func<string, bool> predicate)
        {
            var keys = GetKeys().Where(predicate).ToList();
            foreach (string key in keys)
            {
                Remove(key);
            }
        }



        protected abstract void SetCache(string key, object value, TimeSpan? expireTime = null);
        protected abstract T GetCache<T>(string key);
        protected abstract void RemoveCache(string key);

        /// <summary>
        /// 获取已有缓存键名列表
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        public abstract List<string> GetKeys(Func<string, bool> predicate = null);

        /// <summary>
        /// 判断缓存是否已存在
        /// </summary>
        /// <param name="key">键名</param>
        public abstract bool Contains(string key);

        /// <summary>
        /// 释放缓存对象所点资源
        /// </summary>
        public abstract void Dispose();
    }
}
