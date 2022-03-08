using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wiz.Template.API.ViewModels.ExampleViewModels;

namespace Wiz.Template.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ExampleController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ExampleController> _logger;
        private readonly IMediator _mediator;

        public ExampleController(
            ILogger<ExampleController> logger,
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
        [HttpGet(Name = "GetExample")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IEnumerable<ResponseExampleViewModel> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new ResponseExampleViewModel
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
        public async Task<ActionResult<ResponseExampleViewModel>> GetMediatR(
            [FromBody] RequestExampleViewModel request
        )
        {
            return (await _mediator.Send(request))
                .Match<ActionResult<ResponseExampleViewModel>>(
                    example => Ok(
                        new ResponseExampleViewModel
                        {
                            Date = example.Date,
                            TemperatureC = example.TemperatureC.Value,
                            Summary = example.Summary
                        }
                    ),
                    NotFound()
                );
        }

        /// <summary>
        /// Cria uma nova entrada de temperatura (exemplo) no banco de dados.
        /// </summary>
        /// <param name="request">Exemplo de temperatura.</param>
        /// <returns code="201">Exemplo de temperatura criada.</returns>
        /// <returns code="400">Exemplo de temperatura inválida.</returns>
        /// <returns code="500">Erro interno do servidor.</returns>
        [HttpPost(Name = "Criar exemplo de temperatura")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseExampleViewModel>> CreateExample(
            [FromBody] RequestCreateExampleViewModel request
        )
        {
            return (await _mediator.Send(request))
                .Match<ActionResult<ResponseExampleViewModel>>(
                    example => Created(
                        $"api/v1/Example/{example.Id}",
                        new ResponseExampleViewModel
                        {
                            Date = example.Date,
                            TemperatureC = example.TemperatureC.Value,
                            Summary = example.Summary
                        }
                    ),
                    BadRequest()
                );
        }

        /// <summary>
        /// Exemplo de documentação de parâmetro de rota em endpoint.
        /// </summary>
        /// <param name="param">Parâmetro de exemplo.</param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET Example/route/param/lorem%20ipsum
        ///
        /// </remarks>
        /// <returns>Não possui retorno.</returns>
        /// <response code="204">Nada é retornado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("route/{param}", Name = "Docs Route Param Example")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetRouteParam(
            [FromRoute] string param
        )
        {
            return NoContent();
        }
    }
}
