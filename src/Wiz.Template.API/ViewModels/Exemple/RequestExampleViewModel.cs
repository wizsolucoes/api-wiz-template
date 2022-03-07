using LanguageExt;
using MediatR;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.API.ViewModels.Exemple
{
    public class RequestExampleViewModel : IRequest<Option<Example>>
    {
        public DateTime Date { get; init; }
    }
}
