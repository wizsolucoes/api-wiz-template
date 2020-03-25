# Stage: Development

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as development
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ApplicationInsights:InstrumentationKey=KEY_APPLICATION_INSIGHTS
ENV Azure:KeyVaultUrl=URL_KEY_VAULT
ENV ConnectionStrings:CustomerDB=URL_DB
ENV WizID:Authority=URL_SSO
ENV WizID:Audience=SSO_SCOPE
ENV API:ViaCEP=https://viacep.com.br/ws/
RUN apt update && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

WORKDIR /app

EXPOSE 5000
EXPOSE 5001

COPY . .

RUN dotnet restore
RUN dotnet build "./src/Wiz.Template.API/Wiz.Template.API.csproj" -c Debug
CMD dotnet watch --project src/Wiz.Template.API/Wiz.Template.API.csproj run

# Stage: Staging/Production

FROM development as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet build "./src/Wiz.Template.API/Wiz.Template.API.csproj" -c Release

FROM build AS publish
RUN dotnet publish "./src/Wiz.Template.API/Wiz.Template.API.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS production
ENV ASPNETCORE_ENVIRONMENT=Staging
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "Wiz.Template.API.dll"]
EXPOSE 80