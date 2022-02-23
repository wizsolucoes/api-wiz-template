using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wiz.Template.API.ViewModels.Exemple;

namespace Wiz.Template.API.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet(Name = "GetExemple")]
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

    [HttpGet("mediatr", Name = "MediatR Example")]
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
