using Polly;
using System.Net;
using System.Net.Http.Headers;
using Wiz.Template.Domain.Interfaces.HttpServices;
using Wiz.Template.Infra.HttpServices;

namespace Wiz.Template.API.Extensions
{
    public static class ExternalServiesExtension
    {
        public static void AddExternalServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddHttpClient<IViaCepService, ViaCepService>(
                (s, c) => {
                    c.BaseAddress = new Uri(configuration["API:ViaCep"]);
                    c.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                    );
                }
            ).AddTransientHttpErrorPolicy(
                policyBuilder => policyBuilder.OrResult(
                    response => (int)response.StatusCode ==
                        (int)HttpStatusCode.InternalServerError
                ).WaitAndRetryAsync(
                    3,
                    retry => TimeSpan.FromSeconds(Math.Pow(2, retry))
                )
            ).AddTransientHttpErrorPolicy(
                policyBuilder => policyBuilder.CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                )
            );
        }
    }
}
