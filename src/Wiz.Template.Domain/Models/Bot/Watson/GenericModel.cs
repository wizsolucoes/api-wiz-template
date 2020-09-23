using System.Collections.Generic;

namespace Wiz.Template.Domain.Models.Bot.Watson
{
    public class GenericModel
    {
        public string Response_Type { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public IList<OptionModel> Options { get; set; }
    }
}