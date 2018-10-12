using System;
using Thrift.Server;
using Thrift.Transport;

namespace ThriftTest1.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TServerTransport transport = new TServerSocket(8800);
            var processor = new RuPeng.ThriftTest1.Contract.UserService.Processor(new
            UserServiceImp());
            TServer server = new TThreadPoolServer(processor, transport);
            server.Serve();
            Console.ReadKey();
        }
    }
}
