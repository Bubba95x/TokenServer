using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace API.TokenServer.IdentityEntities
{
    internal class Clients
    {
        public static IEnumerable<Client> Get(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "StatScraperAZF",
                    ClientName = "Stat Scraper AZF",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {
                        new Secret(configuration["AZF:StatsScraper:ClientSecret:Primary"].Sha256()),
                        new Secret(configuration["AZF:StatsScraper:ClientSecret:Secondary"].Sha256())
                    },
                    AllowedScopes = new List<string> { "RocketAPI.Read", "RocketAPI.Write" }
                }
            };
        }
    }
}
