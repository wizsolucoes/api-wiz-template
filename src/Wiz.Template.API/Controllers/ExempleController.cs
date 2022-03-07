using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wiz.Template.API.ViewModels.Exemple;

namespace Wiz.Template.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ExempleController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ExempleController> _logger;
        private readonly IMediator _mediator;

        public ExempleController(
            ILogger<ExempleController> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Exemplo de endpoint básico em ASP.NET.
        /// </summary>
        /// <returns>Um exemplo básico de resposta em JSON.</returns>
        /// <response code="200">Retorno esperado da API.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet(Name = "GetExemple")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IEnumerable<ResponseExempleViewModel> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new ResponseExempleViewModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Exemplo de endpoint operando com MediatR.
        /// </summary>
        /// <returns>Um exemplo básico de resposta em JSON.</returns>
        /// <response code="200">Retorno esperado da API.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("mediatr", Name = "MediatR Example")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseExempleViewModel>> GetMediatR(
            [FromBody] RequestExampleViewModel request
        )
        {
            return (await _mediator.Send(request))
                .Match<ActionResult<ResponseExempleViewModel>>(
                    example => Ok(
                        new ResponseExempleViewModel
                        {
                            Date = example.Date,
                            TemperatureC = example.TemperatureC.Value,
                            Summary = example.Summary
                        }
                    ),
                    NotFound()
                );
        }
    }
}
