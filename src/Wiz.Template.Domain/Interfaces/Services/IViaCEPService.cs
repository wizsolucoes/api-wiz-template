using System.Threading.Tasks;
using Wiz.Template.Domain.Models.Services;

namespace Wiz.Template.Domain.Interfaces.Services
{
    public interface IViaCEPService
    {
        Task<ViaCEP> GetByCEPAsync(string cep);
    }
}
