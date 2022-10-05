using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Wiz.Template.Domain.Interfaces.Notifications;

namespace Wiz.Template.API.Filters;

public class DomainNotificationFilter : IAsyncResultFilter
{
    private readonly IDomainNotification _domainNotification;
    public TelemetryClient _telemetry;

    public DomainNotificationFilter(IDomainNotification domainNotification, TelemetryClient telemetryClient)
    {
        _domainNotification = domainNotification;
        _telemetry = telemetryClient;
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
            
            _telemetry.TrackTrace(new TraceTelemetry(validations, SeverityLevel.Error));
            
            var problemDetails = new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path.Value,
                Detail = validations
            };

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/problem+json";

            await JsonSerializer.SerializeAsync(context.HttpContext.Response.Body, problemDetails);
            
            return;
        }

        await next();
    }
}
