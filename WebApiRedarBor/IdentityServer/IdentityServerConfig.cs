namespace IdentityServer
{
    using Duende.IdentityServer;
    using Duende.IdentityServer.Models;
    using System.Security.Claims;

    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("redarbor.api", "Redarbor API"){
                    UserClaims = new[]
                    {
                        ClaimTypes.Name,
                        ClaimTypes.Email,
                        "userId",
                        "Name"
                    }
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
            new ApiResource("redarbor.api", "Redarbor API")
            {
                Scopes = { "redarbor.api" },
                ApiSecrets = new List<Secret> { new Secret ("secreto".Sha256())}
            }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "redarbor.apiback",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "redarbor.api"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime = 3600,
                }
            };
    }
}