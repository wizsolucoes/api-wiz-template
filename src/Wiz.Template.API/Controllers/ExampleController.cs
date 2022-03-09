using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wiz.Template.API.ViewModels.CepViewModels;
using Wiz.Template.API.ViewModels.ExampleViewModels;
using Wiz.Template.Domain.Models;

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
        [HttpPost("mediatr", Name = "MediatR Example")]
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
        /// Exemplo de documentação de parâmetro de rota em endpoint e de acesso
        /// a serviços externos (ViaCep).
        /// </summary>
        /// <param name="cep">Parâmetro de exemplo.</param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET Example/route/cep/74491615
        ///
        /// </remarks>
        /// <returns>Informações do CEP.</returns>
        /// <response code="200">Informações do CEP.</response>
        /// <response code="404">Cep não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("route/{cep}", Name = "Docs Route Param Example")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseInformacaoCepViewModel>> GetRouteParam(
            [FromRoute] string cep
        )
        {
            var viaCep = await _mediator.Send(
                new RequestInformacaoCepViewModel { Cep = cep }
            );

            return viaCep.Match<ActionResult<ResponseInformacaoCepViewModel>>(
                x =>
                {
                    return !x.Erro ?
                        Ok(
                            new ResponseInformacaoCepViewModel
                            {
                                Logradouro = x.Logradouro,
                                Complemento = x.Complemento,
                                Bairro = x.Bairro,
                                Localidade = x.Localidade,
                                UF = x.UF,
                                Ibge = x.Ibge,
                                Gia = x.Gia,
                                DDD = x.DDD,
                                Siafi = x.Siafi
                            }
                        ) :
                        NotFound();
                },
                BadRequest()
            );
        }
    }
}
