using Microsoft.AspNetCore.Mvc;
using Wiz.Template.API.ViewModels.Message;

namespace Wiz.Template.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v1/notifications")]
    public class NotificationsController : ControllerBase
    {
        public void Post([FromBody]MessageViewModel message)
        {

        }
    }
}