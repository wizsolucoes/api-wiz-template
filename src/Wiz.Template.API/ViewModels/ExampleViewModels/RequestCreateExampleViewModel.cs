using LanguageExt;
using MediatR;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.API.ViewModels.ExampleViewModels
{
    public record RequestCreateExampleViewModel : IRequest<Option<Example>>
    {
        public int TemperatureC { get; init; }
        public string Summary { get; init; }
    }
}
