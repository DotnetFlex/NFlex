using NFlex.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Caching
{
    public interface ICache: IDisposable
    {
        void Set(string key, object value, TimeSpan? expireTime = null);
        T Get<T>(string key);
        void Remove(string key);
        void Remove(Func<string, bool> predicate);
        void RemoveAll();
        List<string> GetKeys(Func<string,bool> predicate=null);
        bool ContainsKey(string key);
    }
}
