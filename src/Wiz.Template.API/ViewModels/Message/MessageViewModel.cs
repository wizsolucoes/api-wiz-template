using Newtonsoft.Json;

namespace Wiz.Template.API.ViewModels.Message
{
    public class MessageViewModel
    {        
        public MessageViewModel()
        {

        }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}