using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Wiz.Template.Domain.Interfaces.Notifications;

namespace Wiz.Template.API.Filters;

public class DomainNotificationFilter : IAsyncResultFilter
{
    private readonly IDomainNotification _domainNotification;

    public DomainNotificationFilter(IDomainNotification domainNotification)
    {
        _domainNotification = domainNotification;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid || _domainNotification.HasNotifications)
        {
            var validations = !context.ModelState.IsValid ?
                JsonSerializer.Serialize(context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)) :
                JsonSerializer.Serialize(_domainNotification.Notifications
                    .Select(x => x.Value));

            var problemDetails = new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path.Value,
                Detail = validations
            };

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/problem+json";

            using var writer = new StreamWriter(context.HttpContext.Response.Body);
            var response = JsonSerializer.Serialize(problemDetails);
            await writer.WriteAsync(response).ConfigureAwait(false);
            await writer.FlushAsync();

        }

        await next();
    }
}
