using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
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
        /*
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
        */

        public int MaxRetryTimes { get; set; } = 0;
        public int RetryIntervalMilliseconds { get; set; } = 100;
        public bool EnableCircuitBreaker { get; set; } = false;
        public int ExceptionsAllowedBeforeBreaking { get; set; } = 3;
        public int MillisecondsOfBreak { get; set; } = 1000;
        public int TimeOutMilliseconds { get; set; } = 0;
        public int CacheTTLMilliseconds { get; set; } = 0;

        private static ConcurrentDictionary<MethodInfo, Policy> policies = new ConcurrentDictionary<MethodInfo, Policy>();

        private static readonly IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            policies.TryGetValue(context.ServiceMethod, out Policy policy);
            lock (policies)
            {
                if (policy == null)
                {
                    policy = Policy.NoOpAsync();
                    if (EnableCircuitBreaker)
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, TimeSpan.FromMilliseconds(MillisecondsOfBreak)));
                    }
                    if (TimeOutMilliseconds > 0)
                    {
                        policy = policy.WrapAsync(Policy.TimeoutAsync(() =>
                        TimeSpan.FromMilliseconds(TimeOutMilliseconds), Polly.Timeout.TimeoutStrategy.Pessimistic));
                    }
                    if (MaxRetryTimes > 0)
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().WaitAndRetryAsync(MaxRetryTimes, i =>
TimeSpan.FromMilliseconds(RetryIntervalMilliseconds)));
                    }

                    Policy policyFallBack = Policy.Handle<Exception>().FallbackAsync(async (ctx, t) =>
                    {
                        AspectContext aspectContext = (AspectContext)ctx["aspectContext"];
                        var fallBackMethod = context.ServiceMethod.DeclaringType.GetMethod(this.FallBackMethod);
                        Object fallBackResult = fallBackMethod.Invoke(context.Implementation, context.Parameters);
                        aspectContext.ReturnValue = fallBackResult;
                    }, async (ex, t) => { });

                    policy = policyFallBack.WrapAsync(policy);
                    policies.TryAdd(context.ServiceMethod, policy);
                }
            }

            Context pollyCtx = new Context();
            pollyCtx["aspectContext"] = context;

            if (CacheTTLMilliseconds > 0)
            {
                string cacheKey = "HystrixMethodCacheManager_Key_" + context.ServiceMethod.DeclaringType + "." + context.ServiceMethod + string.Join("_", context.Parameters);

                if (memoryCache.TryGetValue(cacheKey, out var cacheValue)){
                    context.ReturnValue = cacheValue;
                }
                else
                {
                    await policy.ExecuteAsync(ctx => next(context), pollyCtx);
                    using (var cacheEntry = memoryCache.CreateEntry(cacheKey))
                    {
                        cacheEntry.Value = context.ReturnValue;
                        cacheEntry.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMilliseconds(CacheTTLMilliseconds);
                    }
                }
            }
            else
            {
                await policy.ExecuteAsync(ctx => next(context), pollyCtx);
            }
        }
    }
}
