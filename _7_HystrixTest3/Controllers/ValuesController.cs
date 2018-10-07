using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Mvc;

namespace _7_HystrixTest3.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private Person p;
        public ValuesController(Person p)
        {
            this.p = p;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            //ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            //using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            //{
            //    Person p = proxyGenerator.CreateClassProxy<Person>();
            //    await p.HelloAsync("zhixin");
            //    //Console
            //}
            var a = await p.HelloAsync("zhixin");
            return new string[] { "value11", "value2", a };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
