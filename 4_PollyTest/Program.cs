using Polly;
using System;

namespace _4_PollyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Policy policy = Policy.Handle<ArgumentException>().Fallback(()=> {
                Console.WriteLine("出错了");
            });

            policy.Execute(()=> {
                Console.WriteLine("开始执行");
                throw new ArgumentException();
              });
            Console.ReadKey();
        }
    }
}
