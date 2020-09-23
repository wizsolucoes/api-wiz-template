using System.Collections.Generic;

namespace Wiz.Template.Domain.Models.Bot.Watson
{
    public class MessageRequestModel
    {
        public string Text { get; set; }

        public string UserId { get; set; }

        public Dictionary<string, string> ContextVariables { get; set; }
    }
}