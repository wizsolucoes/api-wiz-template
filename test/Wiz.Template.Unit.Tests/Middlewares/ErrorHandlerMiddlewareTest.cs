using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Wiz.Template.API.Middlewares;
using Wiz.Template.API.Settings;
using Xunit;

namespace Wiz.Template.Unit.Tests.Middlewares
{
    public class ErrorHandlerMiddlewareTest
    {
        private readonly Mock<IOptions<ApplicationInsightsSettings>> _applicationInsightsMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;

        public ErrorHandlerMiddlewareTest()
        {
            _applicationInsightsMock = new Mock<IOptions<ApplicationInsightsSettings>>();
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        }

        [Fact]
        public async Task InvokeErrorHandler_ExceptionTest()
        {
            var applicationInsightsMock = new ApplicationInsightsSettings()
            {
                InstrumentationKey = "118047f1-b165-4bff-9471-e87fd3fe167c"
            };

            _applicationInsightsMock.Setup(x => x.Value)
                .Returns(applicationInsightsMock);

            _webHostEnvironmentMock.Setup(x => x.EnvironmentName)
                .Returns("Development");

            var httpContext = new DefaultHttpContext().Request.HttpContext;
            var exceptionHandlerFeature = new ExceptionHandlerFeature()
            {
                Error = new Exception("Mock error exception")
            };

            httpContext.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);

            var errorHandlerMiddleware = new ErrorHandlerMiddleware(_applicationInsightsMock.Object, _webHostEnvironmentMock.Object);
            await errorHandlerMiddleware.Invoke(httpContext);

            Assert.NotNull(errorHandlerMiddleware);
        }

        [Fact]
        public async Task InvokeErrorHandler_NotExceptionTest()
        {
            var applicationInsightsMock = new ApplicationInsightsSettings()
            {
                InstrumentationKey = "118047f1-b165-4bff-9471-e87fd3fe167c"
            };

            _applicationInsightsMock.Setup(x => x.Value)
                .Returns(applicationInsightsMock);

            _webHostEnvironmentMock.Setup(x => x.EnvironmentName)
                .Returns("Development");

            var httpContext = new DefaultHttpContext().Request.HttpContext;

            var errorHandlerMiddleware = new ErrorHandlerMiddleware(_applicationInsightsMock.Object, _webHostEnvironmentMock.Object);
            await errorHandlerMiddleware.Invoke(httpContext);

            Assert.NotNull(errorHandlerMiddleware);
        }
    }
}
