using LanguageExt;
using MediatR;
using Wiz.Template.API.ViewModels.Example;
using Wiz.Template.Domain.DTOs;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.API.Services
{
    public class GetExampleHandler :
        RequestHandler<RequestExampleViewModel, Option<Example>>
    {
        private List<Example> Examples = new List<Example>
        {
            Example.From(
                new ExampleDTO
                {
                    Id = 1,
                    Date = new DateTime(2022, 2, 2),
                    TemperatureC = 25,
                    Summary = "Mild"
                }
            )
        };

        protected override Option<Example> Handle(
            RequestExampleViewModel request
        )
        {
            return Examples.FirstOrDefault(
                x => x.Date.Equals(request.Date)
            );
        }
    }
}
