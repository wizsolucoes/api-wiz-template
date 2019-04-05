using Microsoft.AspNetCore.Authentication;
using System;
using Wiz.Template.API.Handler;

namespace Wiz.Template.API.Extensions
{
    public static class TokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddBearerToken(this AuthenticationBuilder builder, Action<TokenAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<TokenAuthenticationOptions, BearerTokenAuthenticationHandler>(TokenAuthenticationOptions.Bearer, configureOptions);
        }
    }
}
