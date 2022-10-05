using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;
using Wiz.Template.Domain.Interfaces.Identity;

namespace Wiz.Template.API.Middlewares;

public class LogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TelemetryClient _telemetry;

    public LogMiddleware(RequestDelegate next, TelemetryClient telemetryClient)
    {
        _next = next;
        _telemetry = telemetryClient;
    }

    public async Task Invoke(HttpContext context, IIdentityService identityService)
    {
        var method = context.Request.Method;

        _telemetry.TrackTrace(new TraceTelemetry(identityService.GetScope(), SeverityLevel.Information));

        if (HttpMethods.IsPost(method) || HttpMethods.IsPut(method) || HttpMethods.IsPatch(method))
        {
            var body = await FormatRequestBody(context.Request);

            _telemetry.TrackTrace(new TraceTelemetry(body, SeverityLevel.Information));
        }

        await _next(context);
    }

    private static async Task<string> FormatRequestBody(HttpRequest request)
    {
        string body;
        if (!request.ContentType.Contains("multipart/form-data", StringComparison.InvariantCultureIgnoreCase))
        {
            request.EnableBuffering(bufferThreshold: 1024 * 64);

            body = await GetStringFromPipeReader(request.BodyReader);

            request.Body.Seek(0, SeekOrigin.Begin);
        }
        else
        {
            body = "multipart/form-data";
        }

        return body;
    }

    private static async Task<string> GetStringFromPipeReader(PipeReader reader)
    {
        StringBuilder stringBuilder = new();
        while (true)
        {
            ReadResult read = await reader.ReadAsync();
            ReadOnlySequence<byte> buffer = read.Buffer;

            if (buffer.IsEmpty && read.IsCompleted)
                break;

            foreach (var segment in buffer)
            {
                string asciString = Encoding.UTF8.GetString(
                    segment.Span);
                stringBuilder.Append(asciString);
            }

            reader.AdvanceTo(buffer.End);

            if (read.IsCompleted)
            {
                break;
            }

        }
        return Convert.ToString(stringBuilder);
    }
}
