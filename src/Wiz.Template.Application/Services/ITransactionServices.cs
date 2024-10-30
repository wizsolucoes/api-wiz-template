using Wiz.Template.Application.Features.PostPayment;

namespace Wiz.Template.Application.Services
{
    public interface ITransactionServices
    {
        /// <summary>
        /// Existses the merchant asynchronous.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <returns></returns>
        Task<bool> ExistsMerchantAsync(int  merchantId);

        /// <summary>
        /// Existses the payment method asynchronous.
        /// </summary>
        /// <param name="paymentMethodId">The payment method identifier.</param>
        /// <returns></returns>
        Task<bool> ExistsPaymentMethodAsync(Guid paymentMethodId);

        /// <summary>
        /// Converts to payasync.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        Task<PaymentResponse> ToPayAsync(PaymentRequest input);
    }
}
