using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.Application.Features.GetPaymentsByMerchants;
using Wiz.Template.Application.Features.PostPayment;
using Wizco.Common.Base;
using Wizco.Common.Web;

namespace Wiz.Template.API.Controllers;

[ApiController]
[Route("payments")]
[Authorize]
public class PaymentsController : WizcoControllerBase<PaymentsController>
{
    public PaymentsController(IServiceContext serviceContext, ILogger<PaymentsController> logger, IMediator mediator) : base(serviceContext, logger, mediator)
    {
    }

    /// <summary>
    /// Posts the payment asynchronous.
    /// </summary>
    /// <param name="handler">The handler.</param>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("make-a-payment")]
    public async Task<IServiceResponse<MakePaymentResponse>> MakePaymentAsync([FromServices] MakePaymentHandler handler, MakePaymentRequest request)
    {
        return await handler.ProcessAsync(request);
    }

    [HttpGet]
    [Route("get-by-merchant/{merchantId}")]
    public async Task<IServiceResponse<List<PaymentsByMerchantResponse>>> GetAllByMerchantAsync([FromServices] GetPaymentsByMerchantHandler handler, int merchantId)
    {
        return await handler.ProcessAsync(merchantId);
    }
}