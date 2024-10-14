using Microsoft.Extensions.DependencyInjection;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Infra.Repository;

namespace Wiz.Template.Infra
{
    public static class InfraestructureModule
    {
        public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
