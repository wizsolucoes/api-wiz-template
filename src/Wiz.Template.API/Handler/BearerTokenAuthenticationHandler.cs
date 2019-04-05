using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Wiz.Template.API.Handler
{
    public class BearerTokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        private readonly IConfiguration _configuration;
        private const string BearerScheme = "Bearer";
        private string _exceptionMessage = null;

        public BearerTokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock, IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var bearerToken = authorization.First().Substring(BearerScheme.Length).Trim();

            if (string.IsNullOrEmpty(bearerToken))
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var jwtToken = new JwtSecurityToken(bearerToken);

            if (jwtToken is null)
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new X509SecurityKey(new X509Certificate2(Convert.FromBase64String("MIIC8jCCAdqgAwIBAgIQjZ32s25eQqlLEI+vpR9dsTANBgkqhkiG9w0BAQsFADARMQ8wDQYDVQQDEwZpZC53aXowHhcNMTgwNjE5MjA1MzIwWhcNMzkxMjMxMjM1OTU5WjARMQ8wDQYDVQQDEwZpZC53aXowggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCyLbZAj2qIiTsYJyvUxxYkgyJuIZSkfnEXCtitbZfrBJSvRdEgtxQUSa4VzjsouTfhEHTgV/uC9Ur/JmAsmlyFnKQ7RhDw5NEk6aEF5MaNfp4WngZA7/4AQaxjsQ7tkeCQ5yAudH/ieU0YPsQJOLUcKut6eRmM1pVD7aFdLT/ZYPyyPol5xBDOAvnlAtgk0Y3tt4zQd/DwXmZuV78N4XsrXmVJtLSNqqJCAXMkvR7BEg3K9fPn7TN6Hv2K24FR1e7gax2+jFjQ13LznPJQPFEZ29Gjv2XWIjKwdSQKtHz5BmcVPvrJP5UizUI5ceLsr88YljRFSmprNLTEeJoUjHsBAgMBAAGjRjBEMEIGA1UdAQQ7MDmAEOy3rvfquRiy8dFIswEqkNKhEzARMQ8wDQYDVQQDEwZpZC53aXqCEI2d9rNuXkKpSxCPr6UfXbEwDQYJKoZIhvcNAQELBQADggEBAKaZwJusIJTX6UACj6BAzF3JHmLQ0JRU9dbkYGivXyFI50oEuX7+5zhS7rWM6I07rQw3zzy7ZCfVARBTO2ncu6LoIy49QGIcAnlX6mrxtmfed1Py0zIG0Kkpl43sHeQmFiixUq3g9Tkv1kuRsFVaoG3eT8RaHiPl1BHmjS0KJ6UH0BOd1IZpZqGHy+zes4zosKI6SU5plb3Rs4SxP7ds2DwdVYpEATz3SY+8jcOC7BJo1KqX5WpVd8rEClOdo3vHkaW3kafjCRtptbAZAbzk+jau5GN7ghTQqacaSK6qbkzYmNUdEttyfTGRGoTmv5Ci0ogvS/Ko527G6vMscdhFvyo="))),

                ValidAudience = $"{_configuration["TokenValidationParameters:ValidAudience"]}/resources",
                ValidIssuer = _configuration["TokenValidationParameters:ValidAudience"]
            };

            ClaimsPrincipal jwtClaimPrincipal = null;

            try
            {
                jwtClaimPrincipal = new JwtSecurityTokenHandler().ValidateToken(bearerToken, validationParameters, out var validatedToken);
            }
            catch (Exception ex)
            {
                _exceptionMessage = ex.Message;
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.System, bearerToken)
            };

            var claimsPrincipal = new ClaimsPrincipal(jwtClaimPrincipal);
            claimsPrincipal.AddIdentity(new ClaimsIdentity(claims));

            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(claimsPrincipal), BearerScheme)));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.Headers.Add(HeaderNames.WWWAuthenticate, BearerScheme);

            if (!string.IsNullOrEmpty(_exceptionMessage))
            {
                var exceptionMessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_exceptionMessage));

                Response.Body.Write(exceptionMessage, 0, exceptionMessage.Length);
            }

            return Task.CompletedTask;
        }
    }
}
