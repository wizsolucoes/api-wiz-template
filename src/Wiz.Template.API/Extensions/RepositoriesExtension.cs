using Wiz.Template.Domain.Interfaces.Repositories;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Infra.Repositories;
using Wiz.Template.Infra.UoW;

namespace Wiz.Template.API.Extensions
{
    public static class RepositoriesExtension
    {
        public static void AddRepositoriesServices(
            this IServiceCollection services
        )
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IExampleRepository, ExampleRepository>();
        }
    }
}
