using Microsoft.Extensions.Logging;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Models;
using Wizco.Common.Application;
using Wizco.Common.Base;
using Wizco.Common.Base.Paging;

namespace Wiz.Template.Application.Features.GetAllContacts;

public class GetAllContactsHandler : HandlerBase<ContactsRequest, PagedList<ContactsResponse>>
{
    private readonly IContactsRepository contactsRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllContactsHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public GetAllContactsHandler(ILogger<HandlerBase<ContactsRequest, PagedList<ContactsResponse>>> logger, IContactsRepository contactsRepository) : base(logger)
    {
        this.contactsRepository = contactsRepository;
    }

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
