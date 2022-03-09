using LanguageExt;
using MediatR;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.API.ViewModels.ExampleViewModels
{
    public record RequestCreateExampleViewModel : IRequest<Option<Example>>
    {
        /// <summary>
        /// Temperatura em Celsius
        /// </summary>
        /// <example>25</example>
        public int TemperatureC { get; init; }
        /// <summary>
        /// Resumo da sensação do dia
        /// </summary>
        /// <example>Mild</example>
        public string Summary { get; init; }
    }
}
