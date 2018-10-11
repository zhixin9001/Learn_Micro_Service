using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MsgService
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(option=>
                    {
                        option.Authority = "http://localhost:9500";
                        option.RequireHttpsMetadata = false;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration Configuration, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseAuthentication();

            string ip = Configuration["ip"];
            string port = Configuration["port"];
            string serviceName = "MsgService";
            string serviceId = serviceName + Guid.NewGuid();

            using (var consulClient = new ConsulClient(consulConfig))
            {
                AgentServiceRegistration asr = new AgentServiceRegistration();
                asr.Address = ip;
                asr.Port = Convert.ToInt32(port);
                asr.ID = serviceId;
                asr.Name = serviceName;
                asr.Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    HTTP = $"http://{ip}:{port}/api/Health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5)
                };
                consulClient.Agent.ServiceRegister(asr).Wait();
            };

            appLifetime.ApplicationStopped.Register(() =>
            {
                using (var consulClient = new ConsulClient(consulConfig))
                {
                    Console.WriteLine("应用退出，开始从consul注销");
                    consulClient.Agent.ServiceDeregister(serviceId).Wait();
                }
            });
        }

        private void consulConfig(ConsulClientConfiguration c)
        {
            c.Address = new Uri("http://127.0.0.1:8500");
            c.Datacenter = "dc1";
        }
    }
}
