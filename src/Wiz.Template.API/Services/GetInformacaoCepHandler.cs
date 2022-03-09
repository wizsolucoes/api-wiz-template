using LanguageExt;
using MediatR;
using Wiz.Template.API.ViewModels.CepViewModels;
using Wiz.Template.Domain.Interfaces.HttpServices;
using Wiz.Template.Domain.Models;

namespace Wiz.Template.API.Services
{
    public class GetInformacaoCepHandler :
        IRequestHandler<RequestInformacaoCepViewModel, Option<ViaCep>>
    {
        private readonly IViaCepService _service;

        public GetInformacaoCepHandler(IViaCepService service)
        {
            _service = service;
        }

        public async Task<Option<ViaCep>> Handle(
            RequestInformacaoCepViewModel request,
            CancellationToken cancellationToken
        )
        {
            return await _service.BuscarInformacoesCep(request.Cep);
        }
    }
}
