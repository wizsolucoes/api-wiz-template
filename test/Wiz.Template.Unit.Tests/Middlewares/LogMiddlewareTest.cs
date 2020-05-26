using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Wiz.Template.API.Middlewares;
using Wiz.Template.API.Settings;
using Wiz.Template.Domain.Interfaces.Identity;
using Xunit;

namespace Wiz.Template.Unit.Tests.Middlewares
{
    public class LogMiddlewareTest
    {
        private readonly Mock<IOptions<ApplicationInsightsSettings>> _applicationInsightsMock;
        private readonly Mock<IIdentityService> _identityServiceMock;

        public LogMiddlewareTest()
        {
            _applicationInsightsMock = new Mock<IOptions<ApplicationInsightsSettings>>();
            _identityServiceMock = new Mock<IIdentityService>();
        }

        [Fact]
        public async Task InvokeLogHandler_Test()
        {
            var applicationInsightsMock = new ApplicationInsightsSettings()
            {
                InstrumentationKey = "118047f1-b165-4bff-9471-e87fd3fe167c"
            };

            _applicationInsightsMock.Setup(x => x.Value)
                .Returns(applicationInsightsMock);

            var httpContext = new DefaultHttpContext().Request.HttpContext;

            var logMiddleware = new LogMiddleware(async (innerHttpContext) =>
            {
                await innerHttpContext.Response.WriteAsync("Response body mock");
            }, _applicationInsightsMock.Object);

            await logMiddleware.Invoke(httpContext, _identityServiceMock.Object);

            Assert.NotNull(logMiddleware);
        }
    }
}
