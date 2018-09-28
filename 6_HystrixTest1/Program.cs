using AspectCore.DynamicProxy;
using System;

namespace _6_HystrixTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator= proxyGeneratorBuilder.Build())
            {
                Person p = proxyGenerator.CreateClassProxy<Person>();
                Console.WriteLine(p.HelloAsync("zhixin").Result);
                //Console
            }
                Console.ReadKey();
        }
    }
}
