using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using Polly;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using Wiz.Template.API.Extensions;
using Wiz.Template.API.Filters;
using Wiz.Template.API.Middlewares;
using Wiz.Template.API.Services;
using Wiz.Template.API.Services.Interfaces;
using Wiz.Template.API.Settings;
using Wiz.Template.API.Swagger;
using Wiz.Template.Domain.Interfaces.Identity;
using Wiz.Template.Domain.Interfaces.Notifications;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Interfaces.Services;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Domain.Notifications;
using Wiz.Template.Infra.Context;
using Wiz.Template.Infra.Identity;
using Wiz.Template.Infra.Repository;
using Wiz.Template.Infra.Services;
using Wiz.Template.Infra.UoW;

[assembly: ApiConventionType(typeof(MyApiConventions))]
namespace Wiz.Template.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add<DomainNotificationFilter>();
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = Configuration["WizID:Authority"];
                options.Audience = Configuration["WizID:Audience"];
                options.RequireHttpsMetadata = false;
                options.Events = new JwtBearerEvents
                {
                    //Remover warning caso há alguma validação do token assíncrona (async/await)
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                    OnTokenValidated = async ctx =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
                    {
                        //Exemplo para recuperar informações do token JWT e utilizar no serviço: IIdentityService
                        var jwtClaimScope = ctx.Principal.Claims.FirstOrDefault(x => x.Type == "scope")?.Value;

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.System, jwtClaimScope),
                            new Claim(ClaimTypes.Authentication, ((JwtSecurityToken)ctx.SecurityToken).RawData)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims);
                        ctx.Principal.AddIdentity(claimsIdentity);
                        ctx.Success();
                    }
                };
            });

            services.Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(x =>
            {
                x.Providers.Add<GzipCompressionProvider>();
            });

            services.AddHttpClient<IViaCEPService, ViaCEPService>((s, c) =>
            {
                c.BaseAddress = new Uri(Configuration["API:ViaCEP"]);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.OrResult(response =>
                    !response.IsSuccessStatusCode)
              .WaitAndRetryAsync(3, retry =>
                   TimeSpan.FromSeconds(Math.Pow(2, retry)) +
                   TimeSpan.FromMilliseconds(new Random().Next(0, 100))))
              .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(
                   handledEventsAllowedBeforeBreaking: 3,
                   durationOfBreak: TimeSpan.FromSeconds(30)
            ));

            if (PlatformServices.Default.Application.ApplicationName != "testhost")
            {
                var healthCheck = services.AddHealthChecksUI().AddHealthChecks();

                healthCheck.AddSqlServer(Configuration["ConnectionStrings:CustomerDB"]);

                if (WebHostEnvironment.IsProduction())
                {
                    healthCheck.AddAzureKeyVault(options =>
                    {
                        options.UseKeyVaultUrl($"{Configuration["Azure:KeyVaultUrl"]}");
                    }, name: "azure-key-vault");
                }

                healthCheck.AddApplicationInsightsPublisher();
            }

            if (!WebHostEnvironment.IsProduction())
            {
                services.AddSwaggerDocument(document =>
                {
                    document.DocumentName = "v1";
                    document.Version = "v1";
                    document.Title = "Template API";
                    document.Description = "API de Template";
                    document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                    document.AddSecurity("JWT", Enumerable.Empty<string>(), new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = HeaderNames.Authorization,
                        Description = "Token de autenticação via SSO",
                        In = SwaggerSecurityApiKeyLocation.Header
                    });
                });
            }

            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ApplicationInsightsSettings> options)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseResponseCompression();

            if (PlatformServices.Default.Application.ApplicationName != "testhost")
            {
                app.UseHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }).UseHealthChecksUI(setup =>
                {
                    setup.UIPath = "/health-ui";
                });
            }

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUi3();
            }

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseLogMiddleware();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ErrorHandlerMiddleware(options, env).Invoke
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.Configure<ApplicationInsightsSettings>(Configuration.GetSection("ApplicationInsights"));

            #region Service

            services.AddScoped<ICustomerService, CustomerService>();

            #endregion

            #region Domain

            services.AddScoped<IDomainNotification, DomainNotification>();

            #endregion

            #region Infra

            services.AddDbContext<EntityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CustomerDB")));
            services.AddScoped<DapperContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IIdentityService, IdentityService>();

            #endregion
        }
    }
}
