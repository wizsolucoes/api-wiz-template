using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Wiz.Template.Core.Tests.Mocks.Factory;
using Wiz.Template.Infra.Context;

namespace Wiz.Template.Core.Tests.Fixture
{
    public class WebApplicationFixture<TStartup> : WebApplicationFactory<TStartup>
         where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            IdentityModelEventSource.ShowPII = true;
            builder.ConfigureServices(services =>
            {

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                    typeof(DbContextOptions<EntityContext>));

                services.Remove(descriptor);

                IServiceProvider sp = InitializeServiceProvider(services);
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<EntityContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<WebApplicationFixture<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        new EntityContextSeed(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });


            base.ConfigureWebHost(builder);
        }

        private static IServiceProvider InitializeServiceProvider(IServiceCollection services)
        {
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<EntityContext>(
                    options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        //     .ConfigureWarnings(
                        //         x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)
                        //     );
                        // options.UseInternalServiceProvider(services.BuildServiceProvider());
                    }
                );

            services.AddSingleton<DapperContext>(sp =>
            {
                return new DapperContext(MockRepositoryBuilder.GetMockDbConnection().Object);
            });

            services.PostConfigure<JwtBearerOptions>(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        SignatureValidator = (token, parameters) => new JwtSecurityToken(token),
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidateAudience = false,
                    };

                    options.Configuration = new OpenIdConnectConfiguration();
                });


            return services.BuildServiceProvider(); ;
        }
    }
}
