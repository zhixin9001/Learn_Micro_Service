﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace _8_OcelotTest1
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
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://127.0.0.1:8888")
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.AddJsonFile("configuration.json", false, true);
                });
        }
    }
}
