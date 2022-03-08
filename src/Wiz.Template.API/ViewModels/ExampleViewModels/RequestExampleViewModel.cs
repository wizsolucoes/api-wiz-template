using LanguageExt;
using MediatR;

namespace Wiz.Template.API.ViewModels.ExampleViewModels
{
    public class RequestExampleViewModel :
        IRequest<Option<Wiz.Template.Domain.Entities.Example>>
    {
        /// <summary>
        /// Data de verificação da previsão do tempo
        /// </summary>
        /// <example>2022-03-04T00:00:00.000</example>
        public DateTime Date { get; init; }
    }
}
