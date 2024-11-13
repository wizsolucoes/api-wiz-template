using Microsoft.Extensions.DependencyInjection;
using Wizco.Common.Application;

namespace Wiz.Template.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddTemplateApplication(this IServiceCollection service)
    {
        service.AddApplication();

        return service;
    }
}
