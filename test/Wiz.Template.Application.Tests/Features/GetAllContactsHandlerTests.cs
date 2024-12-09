using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Wiz.Template.Application.Features.GetAllContacts;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repository;
using Wizco.Common.Application;
using Wizco.Common.Base.Paging;

namespace Wiz.Template.Application.Tests.Features;

public class GetAllContactsHandlerTests
{
    private readonly ILogger<HandlerBase<ContactsRequest, PagedList<ContactsResponse>>> logger;
    private readonly IContactsRepository contactsRepository;
    private readonly GetAllContactsHandler handler;

    public GetAllContactsHandlerTests()
    {
        logger = Substitute.For<ILogger<HandlerBase<ContactsRequest, PagedList<ContactsResponse>>>>();
        contactsRepository = Substitute.For<IContactsRepository>();
        handler = new GetAllContactsHandler(logger, contactsRepository);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnTranslatedContacts_WhenRepositoryReturnsData()
    {
        // Arrange
        var request = new ContactsRequest { Page = 1, PageSize = 10 };
        var pagedContacts = new PagedList<Contacts>
        {
            Items = new List<Contacts>
                {
                    new Contacts { Id = 1, RazaoSocial = "John Doe" },
                    new Contacts { Id = 2, RazaoSocial = "Jane Doe" }
                }
        };

        contactsRepository.GetAllAsync(request.Page, request.PageSize).Returns(pagedContacts);

        // Act
        await handler.ProcessAsync(request);

        // Assert
        handler.Response.Data.Items.Should().HaveCount(2);
        handler.Response.Data.Items.Should().ContainEquivalentOf(new ContactsResponse { RazaoSocial = "John Doe" });
        handler.Response.Data.Items.Should().ContainEquivalentOf(new ContactsResponse { RazaoSocial = "Jane Doe" });
    }

    [Fact]
    public async Task HandleAsync_ShouldHandleEmptyResult_WhenRepositoryReturnsNoData()
    {
        // Arrange
        var request = new ContactsRequest { Page = 1, PageSize = 10 };
        var emptyPagedContacts = new PagedList<Contacts> { Items = new List<Contacts>() };
        
        contactsRepository.GetAllAsync(request.Page, request.PageSize).Returns(emptyPagedContacts);

        // Act
        await handler.ProcessAsync(request);

        // Assert
        handler.Response.Data.Items.Should().BeNull();
    }

    [Fact]
    public async Task HandleAsync_ShouldThrowException_WhenRepositoryThrowsException()
    {
        // Arrange
        var request = new ContactsRequest { Page = 1, PageSize = 10 };
        contactsRepository
            .GetAllAsync(request.Page, request.PageSize)
            .Throws(new System.Exception("Repository error"));

        // Act
        var act = async () => await handler.ProcessAsync(request);

        // Assert
        await act.Should().ThrowAsync<System.Exception>()
            .WithMessage("Repository error");
    }
}
