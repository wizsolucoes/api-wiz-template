using Microsoft.Extensions.Logging;
using Wiz.Template.Application.Services;
using Wizco.Common.Application;

namespace Wiz.Template.Application.Features.GetPaymentsByMerchants;

/// <summary>
/// Get Payments By Merchant Handler
/// </summary>
public class GetPaymentsByMerchantHandler(
    ILogger<HandlerBase<int, List<PaymentsByMerchantResponse>>> logger, 
    ITransactionServices transactionServices) : 
        HandlerBase<int, List<PaymentsByMerchantResponse>>(logger)
{
    private ITransactionServices transactionServices = transactionServices;

    protected override async Task HandleAsync()
    {
        this.Response.Data = await this.transactionServices.GetPaymentsByMerchantAsync(this.Input);
    }

    protected override async Task ValidateAsync()
    {
        await Task.CompletedTask;
    }
}
