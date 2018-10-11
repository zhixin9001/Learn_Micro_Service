using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MsgService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            CreateWebhostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebhostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            string ip = config["ip"];
            string port = config["port"];
            //ip="127.0.0.1";
            //port = "5002";
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().UseUrls($"http://{ip}:{port}");
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
