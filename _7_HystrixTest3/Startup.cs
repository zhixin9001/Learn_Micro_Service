using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuPeng.HystrixCore;

namespace _7_HystrixTest3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddSingleton<Person>();
            RegisteServices(this.GetType().Assembly, services);
            return services.BuildAspectInjectorProvider();
        }

        private void RegisteServices(Assembly asm, IServiceCollection service)
        {
            foreach (Type type in asm.GetExportedTypes())
            {
                if (type.GetMethods().Any(m => m.GetCustomAttribute(typeof(HystrixCommandAttribute)) != null))
                {
                    service.AddSingleton(type);
                }

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
