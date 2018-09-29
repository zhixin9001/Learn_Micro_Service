using RuPeng.HystrixCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsgService
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
