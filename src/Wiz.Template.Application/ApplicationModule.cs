using Microsoft.Extensions.DependencyInjection;
using Wiz.Template.Application.Features.GetAllContacts;
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
