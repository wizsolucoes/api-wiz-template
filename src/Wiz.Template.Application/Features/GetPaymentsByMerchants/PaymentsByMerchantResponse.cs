using Wiz.Template.Domain.Models;

namespace Wiz.Template.Application.Features.GetPaymentsByMerchants
{
    public class PaymentsByMerchantResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public int MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the name of the merchant.
        /// </summary>
        /// <value>
        /// The name of the merchant.
        /// </value>
        public string MerchantName { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        public string PaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method.
        /// </summary>
        /// <value>
        /// The name of the payment method.
        /// </value>
        public string PaymentMethodName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the criado em.
        /// </summary>
        /// <value>
        /// The criado em.
        /// </value>
        public DateTime CriadoEm { get; set; }
    }
}
