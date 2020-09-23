using System.Threading.Tasks;
using Wiz.Template.Domain.Models.Services.Blip;

namespace Wiz.Template.Domain.Interfaces.Services
{
    public interface IBlipService
    {
        Task<object> SendMessageAsync(MessageModel message);

        Task<object> PostCommandAsync(CommandModel command);
    }
}