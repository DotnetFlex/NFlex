using Castle.DynamicProxy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
