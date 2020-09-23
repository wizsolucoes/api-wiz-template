using System.Collections.Generic;

namespace Wiz.Template.Domain.Models.Bot.Watson
{
    public class ActionModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Dictionary<string, string> Parameters { get; set; }

        public string Result_Variable { get; set; }
    }
}