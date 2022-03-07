using LanguageExt;
using MediatR;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.API.ViewModels.Exemple
{
    public class RequestExampleViewModel : IRequest<Option<Example>>
    {
        /// <summary>
        /// Data de verificação da previsão do tempo
        /// </summary>
        /// <example>2022-03-04T00:00:00.000</example>
        public DateTime Date { get; init; }
    }
}
