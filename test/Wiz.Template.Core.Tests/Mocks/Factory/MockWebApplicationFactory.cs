using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using Wiz.Template.Infra.Context;


namespace Wiz.Template.Core.Tests.Mocks.Factory
{
    public class MockWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
         where TStartup : class
    {
        private static readonly Lazy<IServiceProvider> LazyServiceProvider = new Lazy<IServiceProvider>();
        public static IServiceProvider GetServiceProvider() => LazyServiceProvider.Value;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            IdentityModelEventSource.ShowPII = true;

            builder.ConfigureServices(
                services =>
                {
                    InitializeServiceProvider(services);
                });
        }

        private static IServiceProvider InitializeServiceProvider(IServiceCollection services)
        {
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<EntityContext>(
                    options =>
                    {
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
                            .ConfigureWarnings(
                                x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)
                            );
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    }
                );

            services.AddSingleton<DapperContext>(sp => new DapperContext(MockRepositoryBuilder.GetMockDbConnection().Object));

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
                        ValidateAudience = false
                    };
                });

            return services.BuildServiceProvider();
        }
    }
}
