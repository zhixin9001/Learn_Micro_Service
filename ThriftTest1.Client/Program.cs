using RuPeng.ThriftTest1.Contract;
using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace ThriftTest1.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TTransport transport = new TSocket("localhost", 8800))
            using (TProtocol protocol = new TBinaryProtocol(transport))
            using (var clientUser = new UserService.Client(protocol))
            using (var clientCalc = new CalcService.Client(protocol))
            {
                transport.Open();
                User u = clientUser.Get(1);
                var a = clientUser.GetAll();

                var calc = clientCalc.Add(2, 4);

                Console.WriteLine($"{u.Id},{u.Name}");
                Console.WriteLine(calc);
            }
            Console.ReadKey();
        }
    }
}
