# Stage: Development

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS development

# Detalhes do PAT_TOKEN no README
ARG nuget_pat={PAT_TOKEN} 
ARG nuget_endpoint=https://pkgs.dev.azure.com/wizsolucoes/_packaging/WizCross/nuget/v3/index.json

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
ENV ASPNETCORE_ENVIRONMENT=Development
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS '{"endpointCredentials":[{"endpoint":"'${nuget_endpoint}'","username":"NoRealUserNameAsIsNotRequired","password":"'${nuget_pat}'"}]}'

RUN apt update && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
RUN curl -sSL https://deb.nodesource.com/setup_10.x | bash --debug
RUN apt-get install nodejs -yq

WORKDIR /app

EXPOSE 5001
EXPOSE 5000

COPY . .

RUN dotnet clean

RUN wget -O - https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | bash
RUN dotnet restore . -s $nuget_endpoint -s "https://api.nuget.org/v3/index.json"

RUN dotnet build "./src/Wiz.Template.API/Wiz.Template.API.csproj" -c Debug
CMD dotnet run --project src/Wiz.Template.API/Wiz.Template.API.csproj

# Stage: Staging/Production

FROM development AS build
WORKDIR /app
COPY . .
RUN dotnet restore . -s $nuget_endpoint -s "https://api.nuget.org/v3/index.json"
RUN dotnet build "./src/Wiz.Template.API/Wiz.Template.API.csproj" -c Release

FROM build AS publish
RUN dotnet publish "./src/Wiz.Template.API/Wiz.Template.API.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS production
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "Wiz.Template.API.dll"]
EXPOSE 80