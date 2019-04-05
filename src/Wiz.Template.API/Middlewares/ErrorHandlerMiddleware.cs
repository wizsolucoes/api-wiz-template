using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Wiz.Template.API.Settings;

namespace Wiz.Template.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly ApplicationInsightsSettings _applicationInsights;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ErrorHandlerMiddleware(IOptions<ApplicationInsightsSettings> options, IHostingEnvironment hostingEnvironment)
        {
            _applicationInsights = options.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            var telemetry = new TelemetryClient
            {
                InstrumentationKey = _applicationInsights.InstrumentationKey
            };

            telemetry.Context.Operation.Id = Guid.NewGuid().ToString();

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (ex == null)
            {
                return;
            }

            telemetry.TrackException(ex);

            var problemDetails = new ProblemDetails
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path.Value,
                Detail = ex.Message
            };

            if (_hostingEnvironment.IsDevelopment())
            {
                problemDetails.Detail += $": {ex.StackTrace}";
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/problem+json";

            using (var writer = new StreamWriter(context.Response.Body))
            {
                new JsonSerializer().Serialize(writer, problemDetails);
                await writer.FlushAsync();
            }
        }
    }
}
