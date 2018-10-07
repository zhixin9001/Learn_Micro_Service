using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _7_HystrixTest2.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]

    public class ValuesController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            {
                Person p = proxyGenerator.CreateClassProxy<Person>();
                await p.HelloAsync("zhixin");
                //Console
            }
            return new string[] { "OK", "OK2" };
        }
    }
}