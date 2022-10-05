using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wiz.Template.API.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly TelemetryClient _telemetry;

    public ErrorHandlerMiddleware(TelemetryClient telemetryClient, IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _telemetry = telemetryClient;
    }

    public async Task Invoke(HttpContext context)
    {
        _telemetry.Context.Operation.Id = Guid.NewGuid().ToString();

        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (ex == null)
        {
            return;
        }

        _telemetry.TrackException(ex);

        var problemDetails = new ProblemDetails
        {
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path.Value,
            Detail = ex.InnerException is null ?
                $"{ex.Message}" :
                $"{ex.Message} | {ex.InnerException}"
        };

        if (_webHostEnvironment.IsDevelopment())
        {
            problemDetails.Detail += $": {ex.StackTrace}";
        }

        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = "application/problem+json";

        var stream = context.Response.Body;
        await JsonSerializer.SerializeAsync(stream, problemDetails);
    }
}
