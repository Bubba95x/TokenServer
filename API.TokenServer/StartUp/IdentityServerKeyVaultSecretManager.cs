using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System.Collections.Generic;
using System.Linq;

namespace API.TokenServer.StartUp
{
    /// <summary>
    /// Pulls down a select list of secrets for this API.  Reduces calls and only pulls secrets it needs in memory
    /// </summary>
    public class IdentityServerKeyVaultSecretManager : DefaultKeyVaultSecretManager
    {
        private readonly IEnumerable<string> secretsToRetrieve;

        public IdentityServerKeyVaultSecretManager()
        {
            // Add to this list as clients and APIs grow
            secretsToRetrieve = new List<string>() {
                "API--Rocket--ApiSecret--Primary",
                "AZF--StatsScraper--ClientSecret--Primary",
                "AZF--StatsScraper--ClientSecret--Secondary"
            };
        }

        public override bool Load(SecretItem secret)
        {
            return secretsToRetrieve.Any(keyString => secret.Identifier.Name.Contains(keyString));
        }
    }
}
