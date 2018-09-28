using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _6_HystrixTest1
{
    public class Person
    {
        [HystrixCommand(nameof(HelloFallBackAsync))]
        public virtual async Task<string> HelloAsync(string name)
        {
            throw new Exception();
            Console.WriteLine("Hello " + name);
            return "ok";
        }

        public async Task<string> HelloFallBackAsync(string name)
        {
            Console.WriteLine("fallBack: " + name);
            return "fail";
        }
    }
}
