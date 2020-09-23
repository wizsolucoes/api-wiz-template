using System;
using Newtonsoft.Json;

namespace Wiz.Template.Domain.Models.Services.Blip
{
    public class MessageModel
    {
        public MessageModel()
        {

        }

        public MessageModel(Guid id, string to, string type, string content)
        {
            Id = id;
            To = to;
            Type = type;
            Content = content;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }

    }
}