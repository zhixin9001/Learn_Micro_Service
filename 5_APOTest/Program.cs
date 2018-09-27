using AspectCore.DynamicProxy;
using System;

namespace _5_APOTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
{
                Person p = proxyGenerator.CreateClassProxy<Person>();
                Console.WriteLine(p.GetType());
                Console.WriteLine(p.GetType().BaseType);
                p.Say("rupeng.com");
            }
            Console.ReadKey();
        }
    }
}
