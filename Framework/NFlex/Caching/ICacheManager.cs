using NFlex.Ioc;
using System;
using System.Collections.Generic;

namespace NFlex.Caching
{
    public interface ICacheManager<TCacheProvider> where TCacheProvider: ICacheProvider
    {
        /// <summary>
        /// 检查指定的缓存键是否存在
        /// </summary>
        bool Exists(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        void Set(string key, object value, TimeSpan? expireTime = default(TimeSpan?));

        /// <summary>
        /// 根据键获取缓存内容
        /// </summary>
        T Get<T>(string key);

        /// <summary>
        /// 根据键获取缓存内容
        /// </summary>
        /// <typeparam name="T">缓存内容类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="addHandler">未获取到缓存内容时要执行的方法</param>
        /// <param name="expireTime">缓存有效期</param>
        T Get<T>(string key, Func<T> addHandler, TimeSpan? expireTime = null);

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        void Remove(string key);

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        void Remove(Func<string, bool> predicate);

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// 获取所有键名
        /// </summary>
        List<string> GetKeys(Func<string, bool> predicate=null);
    }
}
