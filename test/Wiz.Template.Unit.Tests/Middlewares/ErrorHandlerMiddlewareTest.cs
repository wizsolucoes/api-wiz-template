using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Threading.Tasks;
using Wiz.Template.API.Middlewares;
using Xunit;

namespace Wiz.Template.Unit.Tests.Middlewares
{
    public class ErrorHandlerMiddlewareTest
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly HttpContext _httpContext;

        public ErrorHandlerMiddlewareTest()
        {
            _httpContext = new DefaultHttpContext().Request.HttpContext;
            _telemetryClient = new TelemetryClient(new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration());
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();            
            _webHostEnvironmentMock
                .Setup(x => x.EnvironmentName)
                .Returns("Development");
        }

         private ErrorHandlerMiddleware GetErrorHandlerMiddleware()
        {
            return new ErrorHandlerMiddleware(_telemetryClient, _webHostEnvironmentMock.Object);
        }

        [Fact]
        public async Task InvokeErrorHandler_ExceptionTest()
        {
            var exceptionHandlerFeature = new ExceptionHandlerFeature()
            {
                Error = new Exception("Mock error exception")
            };

            _httpContext.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);

            var errorHandlerMiddleware = GetErrorHandlerMiddleware();
            await errorHandlerMiddleware.Invoke(_httpContext);

            Assert.NotNull(errorHandlerMiddleware);
        }

        [Fact]
        public async Task InvokeErrorHandler_NotExceptionTest()
        {
            var errorHandlerMiddleware = GetErrorHandlerMiddleware();
            await errorHandlerMiddleware.Invoke(_httpContext);

            Assert.NotNull(errorHandlerMiddleware);
        }
    }
}
