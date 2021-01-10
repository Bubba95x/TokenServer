using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace API.TokenServer.IdentityEntities
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "Admin",
                UserClaims = new List<string> { "RocketRole" }
            }
        };
        }

        public static IEnumerable<ApiResource> GetApiResources(IConfiguration configuration)
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "RocketAPI",
                    DisplayName = "Rocket API",
                    Description = "Manages Stats for the spicy cheek bois",
                    Scopes = new List<string> { "RocketAPI.Read", "RocketAPI.Write"},
                    ApiSecrets = new List<Secret> { new Secret(configuration["API:Rocket:ApiSecret:Primary"].Sha256()) },
                    UserClaims = new List<string> { "RocketRole" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("RocketAPI.Read", "Read Access to Rocket API"),
                new ApiScope("RocketAPI.Write", "Write Access to Rocket API")
            };
        }
    }
}
