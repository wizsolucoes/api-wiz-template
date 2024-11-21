using Microsoft.Extensions.DependencyInjection;
using Refit;
using Wiz.Template.Application.Clients.OpenFinanceBb;
using Wizco.Common.Application;

namespace Wiz.Template.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddTemplateApplication(this IServiceCollection service)
    {
        service.AddApplication();

        return service;
    }

    public static IServiceCollection AddRefitServices(this IServiceCollection service)
    {
        service
            .AddRefitClient<IOpenRatesService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://opendata.api.bb.com.br"));

        return service;
    }
}
