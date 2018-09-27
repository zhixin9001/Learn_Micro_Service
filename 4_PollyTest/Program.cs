using Polly;
using Polly.Timeout;
using System;
using System.Threading;

namespace _4_PollyTest
{
    class Program
    {
        static async void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Policy policy = Policy.Handle<ArgumentException>().Fallback(()=> {
            //    Console.WriteLine("err occured");
            //});

            //policy.Execute(()=> {
            //    Console.WriteLine("begin execute");
            //    throw new ArgumentException();
            //  });

            //Policy policy = Policy.Handle<ArgumentException>().Retry(5);
            //policy.Execute(() =>
            //{
            //    Console.WriteLine("begin execute");
            //    throw new ArgumentException();
            //});

            //Policy policy = Policy.Handle<ArgumentException>().CircuitBreaker(3, TimeSpan.FromSeconds(3));
            //policy = policy1.Wrap(policy2);

            //while (true)
            //{
            //    Console.WriteLine("begin execute");
            //    try
            //    {
            //        policy.Execute(() =>
            //        {
            //            Console.WriteLine("begin task");
            //            throw new ArgumentException();
            //        });
            //    }
            //    catch
            //    {
            //        Console.WriteLine("err occured");
            //    }
            //}


            //Policy policy1 = Policy.Handle<Exception>().Retry(2);
            //Policy policy2 = Policy.Handle</*Exception*/ TimeoutRejectedException>().Fallback(() =>
            //    {
            //        Console.WriteLine("fallback");
            //    });

            //Policy policy3 = Policy.Timeout(3,Polly.Timeout.TimeoutStrategy.Pessimistic);
            //Policy policy = policy2.Wrap(policy1);  //policy1 was wrapped by policy2

            //Policy policy4 = policy2.Wrap(policy3);

            //policy.Execute(() =>
            //    {
            //        Console.WriteLine("begin task");
            //        throw new ArgumentException();
            //    });

            //policy4.Execute(() =>
            //    {
            //        Console.WriteLine("begin task");
            //        Thread.Sleep(4000);
            //        //throw new ArgumentException();
            //    });


             TestAsync();
            Console.ReadKey();
        }

        private async static int TestAsync()
        {
            Policy policy2 = Policy.Handle</*Exception*/ TimeoutRejectedException>().FallbackAsync(async (c) =>
            {
                Console.WriteLine("fallback");
                return new Task<int>(0);
            });

            Policy policy3 = Policy.TimeoutAsync(3, Polly.Timeout.TimeoutStrategy.Pessimistic);
            Policy policy4 = policy2.WrapAsync(policy3);
          await  policy4.ExecuteAsync(async () =>
            {
                Console.WriteLine("begin task");
                Thread.Sleep(4000);

                //throw new ArgumentException();
            });
        }
    }
}
