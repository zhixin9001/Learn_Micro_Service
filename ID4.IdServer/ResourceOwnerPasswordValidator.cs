using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ID4.IdServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (context.UserName == "zx" && context.Password == "123")
            {
                context.Result = new GrantValidationResult(
                    subject: context.UserName,
                    authenticationMethod: "custom",
                    claims: new Claim[] {
                        new Claim("Name",context.UserName),
                        new Claim("UserId","111"),
                        new Claim("RealName","zhixin"),
                        new Claim("Email","zhixin@9001"),
                    });
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,"Invalid custom credential");
            }
        }
    }
}
