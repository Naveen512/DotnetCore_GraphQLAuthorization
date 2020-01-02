using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace Test.GraphQL.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApi()
        {
            return new List<ApiResource>()
            {
                new ApiResource("graphQLApi", "GraphQL API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Naveen",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha512())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "graphQLApi"
                    },
                    Claims =
                    {
                        new Claim(ClaimTypes.Role,"super admin"),
                        new Claim(ClaimTypes.Role, "Internal admin")
                    }
                }

            };
        }
    }
}
