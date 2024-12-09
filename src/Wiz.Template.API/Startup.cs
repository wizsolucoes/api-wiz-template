using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wizco.Common.Web;
using Wiz.Template.Application;
using Wiz.Template.Infra;
using Microsoft.AspNetCore.Hosting;
using Wiz.SsoConnect.Extension.Sso;
using Wizco.Common.Web.Authentication;

namespace Wiz.Template.API;

public class Startup : WizcoStartupBase
{
    public Startup(IConfiguration config, IWebHostEnvironment webHostEnvironment) : base(config, webHostEnvironment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        //Responsavel pelos handlers [wizco.commons.application]
        services.AddTemplateApplication();

        //Serviços necessarios a aplicação. (opcional)
        services.AddRefitServices();

        services.AddWizApiAuthentication(Configuration, WebHostEnvironment);

        ////Responsavel pela validação do token no sso [sso.connect]
        //services.AddSsoConnectJwt(this.WebHostEnvironment, options =>
        //{
        //    options.Audience = Configuration["WizID:Audience"];
        //});

        //services.AddSsoConnect(this.WebHostEnvironment);

        //seus repositorios e serviços
        services.AddScopedRepositories(Configuration);
    }
}
