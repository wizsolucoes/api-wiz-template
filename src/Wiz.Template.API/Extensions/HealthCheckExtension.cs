using System.Reflection;
using HealthChecks.UI.Client;
using HealthChecks.UI.Core;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Wiz.Template.API.Extensions
{
    public static class HealthCheckExtension
    {
        public static void AddHealthCheckApi(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            if (Assembly.GetEntryAssembly()!.GetName().Name != "testhost")
            {
                var healthCheck = services.AddHealthChecksUI(
                    setupSettings: setup =>
                    {
                        setup.DisableDatabaseMigrations();
                        setup.MaximumHistoryEntriesPerEndpoint(6);
                        setup.AddWebhookNotification(
                            "Teams",
                            configuration["Webhook:Teams"],
                            payload: File.ReadAllText(
                                Path.Combine(".", "MessageCard", "ServiceDown.json")
                            ),
                            restorePayload: File.ReadAllText(
                                Path.Combine(".", "MessageCard", "ServiceRestore.json")
                            ),
                            customMessageFunc: report =>
                            {
                                var failing = report.Entries.Where(
                                    e => e.Value.Status == UIHealthStatus.Unhealthy
                                );
                                return $"{AppDomain.CurrentDomain.FriendlyName}: " +
                                    $"{failing.Count()} healthchecks are failing";
                            }
                        );
                    }
                ).AddInMemoryStorage();

                var healthCheckBuilder = healthCheck.Services.AddHealthChecks();

                // 500 mb
                healthCheckBuilder.AddProcessAllocatedMemoryHealthCheck(
                    500 * 1024 * 1024,
                    "Process Memory",
                    tags: new[] { "self" }
                );

                // 500 mb
                healthCheckBuilder.AddPrivateMemoryHealthCheck(
                    1500 * 1024 * 1024,
                    "Private memory",
                    tags: new[] { "self" }
                );

                healthCheckBuilder.AddSqlServer(
                    configuration["ConnectionStrings:ExampleDB"],
                    tags: new[] { "services" }
                );

                // dotnet add <Project> package AspNetCore.HealthChecks.OpenIdConnectServer
                healthCheckBuilder.AddIdentityServer(
                    new Uri(configuration["WizID:Authority"]),
                    "SSO Wiz",
                    tags: new[] { "services" }
                );

                healthCheckBuilder.AddApplicationInsightsPublisher();
            }
        }

        public static void AddHealthCheckEndpoints(
            this IApplicationBuilder builder
        )
        {
            if (Assembly.GetEntryAssembly()!.GetName().Name != "testhost")
            {
                builder.UseHealthChecks(
                    "/health",
                    new HealthCheckOptions
                    {
                        Predicate = r => r.Tags.Contains("self"),
                        ResponseWriter = UIResponseWriter
                            .WriteHealthCheckUIResponse
                    }
                );

                builder.UseHealthChecks(
                    "/ready",
                    new HealthCheckOptions
                    {
                        Predicate = r => r.Tags.Contains("services"),
                        ResponseWriter = UIResponseWriter
                            .WriteHealthCheckUIResponse
                    }
                );

                builder.UseHealthChecks(
                    "/healthchecks-data",
                    new HealthCheckOptions()
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter
                            .WriteHealthCheckUIResponse
                    }
                );

                builder.UseHealthChecksUI(x => x.UIPath = "/health-ui");
            }
        }
    }
}
