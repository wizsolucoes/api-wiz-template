using Microsoft.Extensions.DependencyInjection;
using Wiz.Template.Application.Services;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Infra.Repository;
using Wiz.Template.Infra.Services;

namespace Wiz.Template.Infra;

public static class InfrastructureModule
{
    public static IServiceCollection AddScopedRepositories(this IServiceCollection service)
    {
        service.AddScoped<IContactsRepository, ContactsRepository>();
        service.AddScoped<IMerchantRepository, MerchantRepository>();
        service.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

        service.AddScoped<ITransactionServices, TransactionServices>();

        return service;
    }
}
