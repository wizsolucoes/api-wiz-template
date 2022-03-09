using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wiz.Template.Infra.Context;

namespace Wiz.Template.API.Extensions
{
    public static class DatabaseExtension
    {
        public static void AddDatabaseService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            if (Assembly.GetEntryAssembly()!.GetName().Name != "testhost")
            {
                services.AddDbContext<EntityContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("ExampleDB")
                    )
                );

                services.AddSingleton<DbConnection>(
                    conn => new SqlConnection(
                        configuration.GetConnectionString("ExampleDB")
                    )
                );

                // services.AddScoped<DapperContext>();
            }
        }
    }
}
