using System;
using System.Threading.Tasks;
using Wiz.Template.Application.Features.PostPayment;
using Wiz.Template.Application.Services;
using Wiz.Template.Domain.Interfaces.Repository;

namespace Wiz.Template.Infra.Services
{
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
        /// Initializes a new instance of the <see cref="TransactionServices"/> class.
        /// </summary>
        /// <param name="merchantRepository">The merchant repository.</param>
        /// <param name="paymentMethodRepository">The payment method repository.</param>
        public TransactionServices(IMerchantRepository merchantRepository, IPaymentMethodRepository paymentMethodRepository)
        {
            this.merchantRepository = merchantRepository;
            this.paymentMethodRepository = paymentMethodRepository;
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
        public async Task<bool> ExistsPaymentMethodAsync(Guid paymentMethodId) =>
            await this.paymentMethodRepository.ExistsByIdAsync(paymentMethodId);

        /// <summary>
        /// Converts to payasync.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<PaymentResponse> ToPayAsync(PaymentRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
