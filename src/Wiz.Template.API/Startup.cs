using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wizco.Common.Application;
using Wizco.Common.DataAccess.Entity;
using Wizco.Common.Web;
using Wiz.Template.Infra;

namespace Wiz.Template.API;

public class Startup : WizcoStartupBase
{
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddApplication();
        services.AddSqlServerContext(Configuration);

        services.AddScopedRepositories();
    }
}
