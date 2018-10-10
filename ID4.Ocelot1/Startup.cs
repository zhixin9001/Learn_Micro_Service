using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ID4.Ocelot1
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Action<IdentityServerAuthenticationOptions> issOptMsg = o =>
            {
                o.Authority = "http://localhost:9500";
                o.ApiName = "MsgAPI";
                o.RequireHttpsMetadata = false;
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = "123321";
            };

            Action<IdentityServerAuthenticationOptions> issOptProduct = o =>
            {
                o.Authority = "http://localhost:9500";
                o.ApiName = "ProductAPI";
                o.RequireHttpsMetadata = false;
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = "123321";
            };

            services.AddAuthorization();
            services.AddOcelot(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOcelot().Wait();
        }
    }
}
