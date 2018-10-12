using RuPeng.ThriftTest1.Contract;
using System;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace ThriftTest1.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //TServerTransport transport = new TServerSocket(8800);
            //var processor = new RuPeng.ThriftTest1.Contract.UserService.Processor(new
            //UserServiceImp());
            //TServer server = new TThreadPoolServer(processor, transport);
            //server.Serve();

            TServerTransport transport = new TServerSocket(8800);
            var processorUserService = new            UserService.Processor(new UserServiceImp());
            var processorCalcService = new CalcService.Processor(new CalcServiceImp());
            var processorMulti = new TMultiplexedProcessor();
            processorMulti.RegisterProcessor("userService", processorUserService);
            processorMulti.RegisterProcessor("calcService", processorCalcService);
            TServer server = new TThreadPoolServer(processorMulti, transport);
            server.Serve();

            Console.WriteLine("listening 8800");
            Console.ReadKey();
        }
    }
}
