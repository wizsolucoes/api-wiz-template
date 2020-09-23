using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wiz.Template.API.Services.Interfaces;
using Wiz.Template.API.ViewModels.Message;

namespace Wiz.Template.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v1/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Envio de mensagem.
        /// </summary>
        /// <param name="message">Parâmetro "message".</param>
        /// <returns>OK.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageViewModel message)
        {
            try
            {
                await _messageService.PostAsync(message);
            }
            catch (System.Exception ex)
            {
                TelemetryClient telemetry = new TelemetryClient(new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY")));
                telemetry.Context.Operation.Id = Guid.NewGuid().ToString();
                telemetry.TrackException(ex);
                return new BadRequestObjectResult(telemetry.Context.Operation.Id);
            }

            return new OkObjectResult(null);
        }
    }
}