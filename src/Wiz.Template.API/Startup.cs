using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wizco.Common.Web;
using Wiz.Template.Application;
using Wiz.Template.Infra;
using Microsoft.AspNetCore.Hosting;
using Wizco.Common.Web.Authentication;
using Wizco.Common.DataAccess;

namespace Wiz.Template.API;

public class Startup : WizcoStartupBase
{
    private readonly IWebHostEnvironment env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration)
    {
        this.env = env;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTemplateApplication();
        services.AddSqlServerContext(Configuration);
        services.AddWizApiAuthentication(Configuration, env);
        services.AddScopedRepositories();
    }
}
