using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Wiz.Template.API
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()/*.SeedData()*/.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                if (context.HostingEnvironment.IsProduction())
                {
                    var buildConfig = config.Build();
                    var vaultUrl = $"{buildConfig["Azure:KeyVaultUrl"]}";
                    var clientId = $"{buildConfig["{CLIENT_ID}"]}";
                    var clientSecret = $"{buildConfig["{CLIENT_SECRET}"]}";

                    config.AddAzureKeyVault(vaultUrl, clientId, clientSecret);
                }
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
