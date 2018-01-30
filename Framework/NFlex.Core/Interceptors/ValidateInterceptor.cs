using Castle.DynamicProxy;

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
            
            //验证通过时继续调用方法
            invocation.Proceed();

        }

        
    }
}
