using Microsoft.Extensions.Logging;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repository;
using Wizco.Common.Application;
using Wizco.Common.Base;
using Wizco.Common.Base.Paging;

namespace Wiz.Template.Application.Features.GetAllContacts;

/// <summary>
/// Initializes a new instance of the <see cref="GetAllContactsHandler"/> class.
/// </summary>
/// <param name="logger">The logger.</param>
public class GetAllContactsHandler(
    ILogger<HandlerBase<ContactsRequest, PagedList<ContactsResponse>>> logger, 
    IContactsRepository contactsRepository) : 
        HandlerBase<ContactsRequest, PagedList<ContactsResponse>>(logger)
{
    private readonly IContactsRepository contactsRepository = contactsRepository;

    protected override async Task HandleAsync()
    {
        PagedList<Contacts> contacts = await this.contactsRepository.GetAllAsync(Input.Page, Input.PageSize);
        this.Response.Data = TranslatorBase.Translate<PagedList<ContactsResponse>, PagedList<Contacts>>(contacts);
    }

    protected override async Task ValidateAsync()
    {
        await Task.CompletedTask;
    }
}
