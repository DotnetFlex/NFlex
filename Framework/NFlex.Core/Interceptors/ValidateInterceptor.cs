using Castle.DynamicProxy;
using NFlex;
using System.Diagnostics;

namespace NFlex.Core.Interceptors
{
    public class ValidateInterceptor: IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //验证方法参数，验证失败时会抛出 ValidationException 异常
            new MethodInvocationValidator(
                invocation.Method,
                invocation.Arguments
                ).Validate();

#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            //验证通过时继续调用方法
            invocation.Proceed();
#if DEBUG
            Debug.WriteLine(string.Format("{0}ms ： {1}.{2}", sw.Stop().TotalMilliseconds, invocation.TargetType.Name, invocation.Method.Name));
#endif
        }

        
    }
}
