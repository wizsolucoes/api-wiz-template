namespace Wiz.Template.Application.Features.PostPayment;

public class MakePaymentRequest
{
    public int MerchantId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethodId { get; set; }
}
