using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wiz.Template.Infra.Context;

namespace Wiz.Template.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<EntityContext>();

                context.Database.Migrate();

                new EntityContextSeed().SeedInitial(context);
            }

            return host;
        }
    }
}
