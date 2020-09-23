using Wiz.Template.Domain.Models.Bot.Watson;

namespace Wiz.Template.Domain.Interfaces.Bot
{
    public interface IWatsonBot
    {
        ResponseModel SendMessage(MessageRequestModel message);
    }
}