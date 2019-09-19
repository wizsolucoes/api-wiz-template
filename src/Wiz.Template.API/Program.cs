using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System.Diagnostics.CodeAnalysis;
using Wiz.Template.API.Extensions;

namespace Wiz.Template.API
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()/*.SeedData()*/.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((context, config) =>
                 {
                     if (context.HostingEnvironment.IsProduction() &&
                        PlatformServices.Default.Application.ApplicationName != "testhost")
                     {
                         var buildConfig = config.Build();
                         var vaultUrl = $"{buildConfig["Azure:KeyVaultUrl"]}";
                         var clientId = $"{buildConfig["{CLIENT_ID}"]}";
                         var clientSecret = $"{buildConfig["{CLIENT_SECRET}"]}";

                         config.AddAzureKeyVault(vaultUrl, clientId, clientSecret);
                     }
                 })
                .UseApplicationInsights()
                .UseStartup<Startup>();
    }
}
