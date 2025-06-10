namespace Wiz.Template.Application.Features.PostMakePayment;

public class MakePaymentResponse
{
    public Guid TransactionId { get; set; }

    public string RateValue { get; set; }
    
    public string RateDisclaimer { get; set; }
}
