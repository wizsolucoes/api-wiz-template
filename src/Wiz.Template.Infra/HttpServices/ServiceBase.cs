using System.Text;
using LanguageExt;
using Newtonsoft.Json;

namespace Wiz.Template.Infra.HttpServices
{
    public class ServiceBase
    {
        public StringContent AssembleJsonContent(object model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            return new StringContent(
                jsonContent,
                Encoding.UTF8,
                "application/json"
            );
        }

        public async Task<Option<TModel>> HandleResponse<TModel>(
            HttpResponseMessage response
        )
        {
            if (response.IsSuccessStatusCode)
            {
                return await HandleSuccessResponse<TModel>(response);
            }

            // TODO Tratar erro na requisição!
            return Option<TModel>.None;
        }

        private async Task<Option<TModel>> HandleSuccessResponse<TModel>(
            HttpResponseMessage response
        )
        {
            var json = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<TModel>(json);

            return model is not null ?
                model :
                Option<TModel>.None;
        }
    }
}
