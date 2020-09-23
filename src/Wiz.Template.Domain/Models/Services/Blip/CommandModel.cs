using Newtonsoft.Json;

namespace Wiz.Template.Domain.Models.Services.Blip
{
    public class CommandModel
    {
        public CommandModel() { }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}