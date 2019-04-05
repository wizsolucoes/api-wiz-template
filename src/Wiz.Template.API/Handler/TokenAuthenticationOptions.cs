using Microsoft.AspNetCore.Authentication;

namespace Wiz.Template.API.Handler
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string Bearer = "Bearer";
    }
}
