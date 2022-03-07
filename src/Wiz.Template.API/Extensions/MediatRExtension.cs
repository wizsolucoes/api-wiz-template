using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Wiz.Template.API.Extensions
{
    public static class MediatRExtension
    {
        public static void AddMediatRApi(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program));
        }
    }
}
