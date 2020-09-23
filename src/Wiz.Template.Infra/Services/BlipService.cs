using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wiz.Template.Domain.Interfaces.Services;
using Wiz.Template.Domain.Models.Services.Blip;

namespace Wiz.Template.Infra.Services
{
    public class BlipService : IBlipService
    {
        private readonly HttpClient _httpClient;

        public BlipService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> SendMessageAsync(MessageModel message)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("messages", stringContent);

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<object> PostCommandAsync(CommandModel command)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var resultResponse = await _httpClient.PostAsync("commands", stringContent);

            return await resultResponse.Content.ReadAsStringAsync();
        }
    }
}