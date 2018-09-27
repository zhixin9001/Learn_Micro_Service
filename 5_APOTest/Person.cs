using System;
using System.Collections.Generic;
using System.Text;

namespace _5_APOTest
{
    public class Person
    {
        [CustomInterceptor]
        public virtual void Say(string msg)
        {
            Console.WriteLine("service calling..." + msg);
        }
    }
}
