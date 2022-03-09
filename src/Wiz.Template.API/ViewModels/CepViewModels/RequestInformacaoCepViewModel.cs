using LanguageExt;
using MediatR;
using Wiz.Template.Domain.Models;

namespace Wiz.Template.API.ViewModels.CepViewModels
{
    public record RequestInformacaoCepViewModel : IRequest<Option<ViaCep>>
    {
        public string Cep { get; init; }
    }
}
