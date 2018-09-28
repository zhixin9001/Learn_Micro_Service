using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _6_HystrixTest1
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HystrixCommandAttribute : AbstractInterceptorAttribute
    {
        public string FallBackMethod { get; set; }
        public HystrixCommandAttribute(string fallBackMethod)
        {
            this.FallBackMethod = fallBackMethod;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var fallBackMethod = context.ServiceMethod.DeclaringType.GetMethod(this.FallBackMethod);
                Object fallBackResult = fallBackMethod.Invoke(context.Implementation, context.Parameters);
                context.ReturnValue = fallBackResult;
            }
        }
    }
}
