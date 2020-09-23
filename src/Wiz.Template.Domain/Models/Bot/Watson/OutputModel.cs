using System.Collections.Generic;

namespace Wiz.Template.Domain.Models.Bot.Watson
{
    public class OutputModel
    {
        public IList<GenericModel> Generic { get; set; }
        public IList<IntentModel> Intents { get; set; }
        public IList<object> Entities { get; set; }

        public IList<ActionModel> Actions { get; set; }
    }
}