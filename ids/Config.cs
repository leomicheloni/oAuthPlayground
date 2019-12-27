using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace ids
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("customAPI", "Custom Web API")
                {
                    ApiSecrets = { new Secret ("secret".Sha256()) }
                },
                new ApiResource("api1", "Otra API"),
                new ApiResource("read", "read rights"),
                new ApiResource("write", "write rigths"),
            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser
                {
                    Password = "mypass",
                    Username = "alice",
                    SubjectId = "alice@alice.com",
                    Claims = new List<System.Security.Claims.Claim>
                    {
                        new System.Security.Claims.Claim("family_name", "perez"),
                        new System.Security.Claims.Claim("role", "admin"),
                        new System.Security.Claims.Claim("groups", System.Guid.Empty.ToString()),
                        new System.Security.Claims.Claim("oid", new System.Guid().ToString())
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ro.client",
                    ClientName = "Resource owner password",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "customAPI"
                    },
                    ClientSecrets = { new Secret ( "secret".Sha256())}
                },
                new Client
                {
                    ClientId = "ro.client.refresh",
                    ClientName = "Resource owner password with refresh",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "customAPI"
                    },
                    AllowOfflineAccess = true,
                    ClientSecrets = { new Secret ( "secret".Sha256())}
                },
                new Client
                {
                    ClientId = "cc.client.oauth",
                    ClientName = "Client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret ( "secret".Sha256())},

                    AllowedScopes = new List<string>
                    {
                        "customAPI"
                    }
                }
            };
        }
    }
}
