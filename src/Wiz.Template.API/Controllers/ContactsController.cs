using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Wiz.Template.Application.Features.GetAllContacts;
using Wizco.Common.Base;
using Wizco.Common.Base.Paging;
using Wizco.Common.Web;

namespace Wiz.Template.API.Controllers;

[ApiController]
[Route("contacts")]
[Authorize]
public class ContactsController : WizcoControllerBase<ContactsController>
{
    public ContactsController(IServiceContext serviceContext, ILogger<ContactsController> logger, IMediator mediator) : base(serviceContext, logger, mediator)
    {
    }

    /// <summary>
    /// Gets all.
    /// </summary>
    /// <param name="handler">The handler.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IServiceResponse<PagedList<ContactsResponse>>> GetAll([FromServices] GetAllContactsHandler handler, ContactsRequest request)
    {
        return await handler.ProcessAsync(request);
    }
}