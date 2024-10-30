namespace Wiz.Template.Application.Features.PostPayment;

public class PaymentRequest
{
    public int MerchantId { get; set; }

    public decimal Amount { get; set; }

    public Guid PaymentMethodId { get; set; }
}
