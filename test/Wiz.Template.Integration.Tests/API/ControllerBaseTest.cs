using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wiz.Template.Integration.Tests.API
{
    public abstract class ControllerBaseTest
    {
        protected async Task<TEntity> DeserializeObject<TEntity>(
            HttpResponseMessage response
        )
        {
            return JsonConvert.DeserializeObject<TEntity>(
                    await response.Content.ReadAsStringAsync()
                )!;
        }
    }
}
