using Consul;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ServiceConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             服务治理：注册、注销、健康检查
             服务发现
             */
            using (var consul = new ConsulClient(c => { c.Address = new Uri("http://127.0.0.1:8500"); }))
            {
                var services = consul.Agent.Services().Result.Response;
                foreach (var s in services.Values)
                {
                    Console.WriteLine($"id={s.ID},service={s.Service},addr={s.Address},port={s.Port}");
                }

                //客户端负载均衡
                var service = services.First();

                //消费服务
                using (var http = new HttpClient())
                {
                    var content = new StringContent($"addr={service.Value.Address},port={service.Value.Port}", Encoding.UTF8, "application/json");
                    var s= http.PostAsync($"http://{service.Value.Address}:{service.Value.Port}/api/SMS", content).Result;
                    Console.WriteLine(s.RequestMessage);
                }
            };
            Console.ReadKey();
        }
    }
}
