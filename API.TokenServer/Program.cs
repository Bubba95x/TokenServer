using API.TokenServer.StartUp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace API.TokenServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();                    

                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        var settings = config.Build();
                        config.AddAzureKeyVault(new AzureKeyVaultConfigurationOptions() {
                            Client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback)),
                            Manager = new IdentityServerKeyVaultSecretManager(),
                            Vault = settings["keyvaulturl"]
                        });
                    });                    
                });
    }
}
