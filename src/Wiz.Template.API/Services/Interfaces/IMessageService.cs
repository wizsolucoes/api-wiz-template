using System.Threading.Tasks;
using Wiz.Template.API.ViewModels.Message;

namespace Wiz.Template.API.Services.Interfaces
{
    public interface IMessageService
    {
        Task<string> PostAsync(MessageViewModel message);

    }
}