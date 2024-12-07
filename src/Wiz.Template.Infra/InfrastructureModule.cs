using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wiz.Template.Application.Services;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Infra.Repository;
using Wiz.Template.Infra.Services;
using Wizco.Common.DataAccess;

namespace Wiz.Template.Infra;

public static class InfrastructureModule
{
    public static IServiceCollection AddScopedRepositories(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        service.AddSqlServerContext((provider, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
            options.AddInterceptors(provider.GetRequiredService<ISaveChangesInterceptor>());
        });

        service.AddScoped<IContactsRepository, ContactsRepository>();
        service.AddScoped<IMerchantRepository, MerchantRepository>();
        service.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        service.AddScoped<ITransactionRepository, TransactionRepository>();

        service.AddScoped<ITransactionServices, TransactionServices>();

        return service;
    }
}
