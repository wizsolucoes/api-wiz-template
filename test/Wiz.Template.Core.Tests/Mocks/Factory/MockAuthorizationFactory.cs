using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Wiz.Template.Core.Tests.Mocks.Factory
{
    public class MockAuthorizationFactory
    {

        private const string AuthorizationHeader = "Authorization";


        private static string GerarTokenJWT(IEnumerable<Claim> claims)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.{PlatformServices.Default.Application.ApplicationName}.json")
                    .Build();

                var issuer = configuration["WizID:Authority"];
                var audience = configuration["WizID:Audience"];
                var expiry = DateTime.Now.AddMinutes(300);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("only_test#Desta maneira, o desenvolvimento contínuo de distintas formas de atuação promove a alavancagem das condições inegavelmente apropriadas.#only_test"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(issuer: issuer, audience: audience,
    expires: expiry, signingCredentials: credentials, claims: claims);
                var tokenHandler = new JwtSecurityTokenHandler();
                var stringToken = tokenHandler.WriteToken(token);
                return stringToken;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void AddAdminHeaders(HttpClient client)
        {
            string adminJwtToken = GerarTokenJWT(new List<Claim> {
                new Claim("enterprise", Guid.Empty.ToString()),
                 new Claim("cnpj", "42278473000103"),
                 new Claim("scope", "api.full_access"),
                 new Claim("scope", "api-terceiro.un"),
                });

            client.DefaultRequestHeaders.TryAddWithoutValidation(AuthorizationHeader, $"Bearer {adminJwtToken}");
        }

        public static void AddAnonymousHeaders(HttpClient client)
        {
            if (client.DefaultRequestHeaders.Contains(AuthorizationHeader))
            {
                client.DefaultRequestHeaders.Remove(AuthorizationHeader);
            }
        }
    }
}
