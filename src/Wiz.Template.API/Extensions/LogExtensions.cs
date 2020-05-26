using Microsoft.AspNetCore.Builder;
using Wiz.Template.API.Middlewares;

namespace Wiz.Template.API.Extensions
{
    public static class LogExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
