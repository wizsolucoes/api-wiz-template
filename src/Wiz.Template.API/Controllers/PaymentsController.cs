using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
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
    public async Task<IServiceResponse<PaymentResponse>> PostPaymentAsync([FromServices] PaymentHandler handler, PaymentRequest request)
    {
        return await handler.ProcessAsync(request);
    }
}