using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ID4.LoginService.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> RequestToken(RequestTokenParam token)
        {
            var dict = new Dictionary<string, string>();
            dict["client_id"] = "clientPC";
            dict["client_secret"] = "123321";
            dict["grant_type"] = "password";
            dict["username"] = "zx";
            dict["password"] = "123";

            using (HttpClient http = new HttpClient())
            using (var content = new FormUrlEncodedContent(dict))
            {
                var msg = await http.PostAsync("http://localhost:9500/connect/token", content);
                string result = await msg.Content.ReadAsStringAsync();
                return Content(result,"application/json");
            }
        }

    }
}