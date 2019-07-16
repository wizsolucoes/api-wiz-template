using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Wiz.Template.Domain.Interfaces.Identity;

namespace Wiz.Template.Infra.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetScope() => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.System)?.Value;
    }
}