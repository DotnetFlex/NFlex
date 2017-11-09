using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Ioc
{
    public interface IDisposableDependencyWrapper<out T>:IDisposable
    {
        T Object { get; }
    }

    internal class DisposableDependencyWrapper<T> : IDisposableDependencyWrapper<T>
    {
        public T Object { get; private set; }

        public DisposableDependencyWrapper(T obj)
        {
            Object = obj;
        }

        public void Dispose()
        {
            IocManager.Release(Object);
        }
    }
}
