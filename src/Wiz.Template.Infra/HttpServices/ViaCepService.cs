using LanguageExt;
using Wiz.Template.Domain.Interfaces.HttpServices;
using Wiz.Template.Domain.Models;
using Newtonsoft.Json;

namespace Wiz.Template.Infra.HttpServices
{
    public class ViaCepService : ServiceBase, IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Option<ViaCep>> BuscarInformacoesCep(string cep)
        {
            var response = await _httpClient.GetAsync(
                $"{cep}/json",
                HttpCompletionOption.ResponseContentRead
            );

            return await HandleResponse<ViaCep>(response);
        }
    }
}
