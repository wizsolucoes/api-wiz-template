using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiz.Template.Application.Clients.OpenFinanceBb;
using Wiz.Template.Application.Features.GetPaymentsByMerchants;
using Wiz.Template.Application.Features.PostMakePayment;
using Wiz.Template.Application.Services;
using Wiz.Template.Domain.Interfaces.Repository;

namespace Wiz.Template.Infra.Services;

public class TransactionServices : ITransactionServices
{
    /// <summary>
    /// The merchant repository
    /// </summary>
    private IMerchantRepository merchantRepository;

    /// <summary>
    /// The payment method repository
    /// </summary>
    private IPaymentMethodRepository paymentMethodRepository;

    /// <summary>
    /// The transaction repository
    /// </summary>
    private ITransactionRepository transactionRepository;

    /// <summary>
    /// The open rate service
    /// </summary>
    private IOpenRatesService _openRateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionServices"/> class.
    /// </summary>
    /// <param name="merchantRepository">The merchant repository.</param>
    /// <param name="paymentMethodRepository">The payment method repository.</param>
    /// <param name="transactionRepository">The transaction repository.</param>
    public TransactionServices(
        IMerchantRepository merchantRepository,
        IPaymentMethodRepository paymentMethodRepository,
        ITransactionRepository transactionRepository,
        IOpenRatesService openRateService)
    {
        this.merchantRepository = merchantRepository;
        this.paymentMethodRepository = paymentMethodRepository;
        this.transactionRepository = transactionRepository;
        _openRateService = openRateService;
    }

    /// <summary>
    /// Existses the merchant asynchronous.
    /// </summary>
    /// <param name="merchantId">The merchant identifier.</param>
    /// <returns></returns>
    public async Task<bool> ExistsMerchantAsync(int merchantId) => 
        await this.merchantRepository.ExistsByIdAsync(merchantId);

    /// <summary>
    /// Existses the payment method asynchronous.
    /// </summary>
    /// <param name="paymentMethodId">The payment method identifier.</param>
    /// <returns></returns>
    public async Task<bool> ExistsPaymentMethodAsync(string paymentMethodId) =>
        await this.paymentMethodRepository.ExistsByIdAsync(paymentMethodId);

    /// <summary>
    /// Gets the payments by merchant asynchronous.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    public async Task<List<PaymentsByMerchantResponse>> GetPaymentsByMerchantAsync(int input)
    {
        var transactions = await this.transactionRepository.GetPaymentsByMerchantAsync(input);

        return transactions.Select(x => new PaymentsByMerchantResponse
        {
            Id = x.Id,
            MerchantId = x.MerchantId,
            Amount = x.Amount,
            ExternalId = x.ExternalId,
            CriadoEm = x.CriadoEm,
            PaymentMethodId = x.PaymentMethodId,
            MerchantName = x.Merchant.Name,
            PaymentMethodName = x.PaymentMethod.Name,
        }).ToList();
    }
        

    /// <summary>
    /// Converts to payasync.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public async Task<MakePaymentResponse> ToPayAsync(MakePaymentRequest input)
    {
        RateResponse rates = await this._openRateService.GetOnlineRates();

        Wiz.Template.Domain.Models.Transaction payment = new()
        {
            Amount = input.Amount,
            MerchantId = input.MerchantId,
            PaymentMethodId = input.PaymentMethodId,
            CriadoEm = DateTime.Now,
            Status = "Em Processamento",
            ExternalId = Guid.NewGuid().ToString() 
        };

        await this.transactionRepository.AddAsync<Wiz.Template.Domain.Models.Transaction>(payment);

        return new()
        {
            TransactionId = payment.Id,
            RateValue = rates.Data.FirstOrDefault().Value,
            RateDisclaimer = rates.Data.FirstOrDefault().Disclaimer,
        };
    }
}
