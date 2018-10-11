using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MsgService.Controllers
{
    [Produces("application/json")]
    [Route("api/Default")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            string name = this.User.Identity.Name;//            读取的就是"Name"这个特殊的Claims的值
            //string userId = this.User.FindFirst("UserId").Value;
            //string realName = this.User.FindFirst("RealName").Value;
            //string email = this.User.FindFirst("Email").Value;
            Console.WriteLine("名称"+name);
            //Console.WriteLine($"name={name},userId={userId},realName={realName},email={email}");
            //ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            //using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            //{
            //    Person p = proxyGenerator.CreateClassProxy<Person>();
            //    await p.HelloAsync("zhixin");
            //}
            return new string[] { "a", "b" };
        }
    }
}