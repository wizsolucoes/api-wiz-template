using LanguageExt;
using Wiz.Template.Domain.Models;

namespace Wiz.Template.Domain.Interfaces.HttpServices
{
    public interface IViaCepService
    {
        Task<Option<ViaCep>> BuscarInformacoesCep(string cep);
    }
}
