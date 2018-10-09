using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ID4.IdServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> resources = new List<ApiResource>();
            //ApiResource第一个参数是应用的名字，第二个参数是描述
            resources.Add(new ApiResource("MsgAPI", "消息服务API"));
            resources.Add(new ApiResource("ProductAPI", "产品API"));
            return resources;
        }

        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            clients.Add(new Client
            {
                ClientId = "clientPC1",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {
                    new Secret("123321".Sha256())
                },
                AllowedScopes = { "MsgAPI", "ProductAPI" }
            });
            return clients;
        }
    }
}
