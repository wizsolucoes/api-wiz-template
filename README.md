# Wiz - API

- [Desenvolvimento, por onde começar](#desenvolvimento-por-onde-começar)
- [Execução do projeto](#execução-do-projeto)
- [Estrutura](#estrutura)
- [Dependências](#dependências)
- [Build e testes](#build-e-testes)
- [NuGet privado](#nuget-privado)
- [CI/CD](#ci/cd)
- [README](#readme)

## Desenvolvimento, por onde começar

Passos para execução do projeto:

1. Abrir *Prompt de Comando* de sua preferência (**CMD** ou **PowerShell**);

2. Criar pasta para o projeto no local desejado;

3. Executar os seguintes comandos;

  > *dotnet new -i Wiz.Dotnet.Template.API --nuget-source https://api.nuget.org/v3/index.json*
  
  > *dotnet new wizapi -n [NomeProjeto]*

4. Executar comando para configurar aplicação em modo **(HTTPS)**;

  > *dotnet dev-certs https --trust*

5. Incluir configurações de *varíaveis de ambiente* no caminho abaixo:

### **Local**

```console
├── src (pasta física)
  ├── Wiz.[NomeProjeto].API (projeto)
    ├── appsettings.{ENVIRONMENT}.json
```

Dentro do arquivo *local.settings.json*, há o conteúdo para modificação das variáveis:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information"
    }
  },
  "ApplicationInsights": {
    "ConnectionString": "<appinsights-connection-string>"
  },
  "AppConfiguration": {
    "Prefix": "WizTemplate:*",
    "Sentinel": "WizTemplate:Sentinel",
    "CacheExpirationFromMinutes": 15
  },
  "ConnectionStrings": {
    "AppConfig": "<app-configuration-connection-string>",
    "Database": "<data-base-connection-string>",
    "Redis": "<redis-connection-string>"
  },
  "SwaggerSettings": {
    "Name": "latest",
    "Title": "WizTemplate API",
    "Description": "WizTemplate API",
    "Version": "latest"
  },
  "WizID": {
    "Authority": "<valor>",
    "Issues": "<valor>",
    "Audience": "<valor>",
    "Audiences": "<valor>"
  },
  "WProApi": {
    "BaseAddress": "<valor>"
  }
}

```

6. Inserir chave do **Application Insights** conforme configurado no Azure no arquivo *appsettings.{ENVIRONMENT}.json*.

### **Docker + nuget**

```console
├── Dockerfile
```

Para utilizar o Nuget privado (Wiz Common) é necessário realizar autenticação no Azure DevOps via **PAT (Personal Access Tokens)**.

Para gerar o token é necessário seguir os seguintes passos:

1. Entrar em configurações de token do [Azure DevOps](https://dev.azure.com/wizsolucoes/_usersSettings/tokens)

2. Clicar na opção **New Token**

3. Inserir um nome desejado do token

4. Inserir a data de inspiração desejada (recomendado 90 dias)

5. Em **Scopes** selecionar em **Packaging** a opção **Read & write**


## Estrutura

Padrão das camadas do projeto:

1. **Wiz.[NomeProjeto].Domain**: domínio da aplicação, responsável de manter as *regras de negócio* para a API;
2. **Wiz.[NomeProjeto].Infra**: camada mais baixa, para acesso a dados, infraestrutura e serviços externos;
3. **Wiz.[NomeProjeto].Application**: camada mais baixa, para acesso a dados, aplicação e manipulação de fluxos;
4. **Wiz.[NomeProjeto].API**: responsável pela camada de *disponibilização* dos endpoints da API;

Formatação do projeto dentro do repositório:

```console
├── src 
  ├── Wiz.[NomeProjeto].Domain (projeto)
  ├── Wiz.[NomeProjeto].Infra (projeto)
  ├── Wiz.[NomeProjeto].Application (projeto)
  ├── Wiz.[NomeProjeto].API (projeto)
├── test
├── Wiz.[NomeProjeto] (solução)
```

### **Build com Visual Studio**

1. Comandos para geração de build:

- Debug: Executar via Test Explorer (adicionar breakpoint)
- Release: Executar via Test Explorer (não adicionar breakpoint)

2. Ativar funcionalidade [Live Unit Testing](https://docs.microsoft.com/en-us/visualstudio/test/live-unit-testing?view=vs-2017) para executar testes em tempo de desenvolvimento (execução) do projeto.

3. Ativar funcionalidade [Code Coverage](https://docs.microsoft.com/en-us/visualstudio/test/using-code-coverage-to-determine-how-much-code-is-being-tested?view=vs-2017) para cobertura de testes.

As funcionalidades **Live Unit Testing** e **Code Coverage** estão disponíveis apenas na versão **Enterprise** do Visual Studio.

### **Build com Visual Studio Code**

1. Instalar o pacote [DevZ - Back-end Pack
](https://marketplace.visualstudio.com/items?itemName=WizSolucoes.devz-back-end-pack)

2. Executar **task** (ctrl + shift + p) e digite **Run Test Task**:

- *test* - Executar projeto de testes
- *test with coverage* - Executar projeto de testes com cobertura

3. Ativar **Watch** na parte inferior do Visual Studio Code para habilitar cores nas classes que descrevem a cobertura. É necessário executar os testes no modo *test with coverage*.

4. O relatório dos testes são gerados automaticamente na pasta **code_coverage** localizada na pasta **test** sempre que for executado o comando a task *test with coverage*.

## NuGet privado

### **Visual Studio + nuget**

1. Adicionar *url* do NuGet privado no caminho do *menu* abaixo:

```console
Tools -> NuGet Package Manager -> Package Sources
```

### **Visual Studio Code + nuget**

1. Abrir *Prompt de Comando* de sua preferência (**CMD** ou **PowerShell**) ou utilizar o terminal do Visual Studio Code;

2. Executar script Powershell para adicionar permissão do NuGet na máquina local:

- https://github.com/microsoft/artifacts-credprovider/blob/master/helpers/installcredprovider.ps1 (Windows);
- https://github.com/microsoft/artifacts-credprovider/blob/master/helpers/installcredprovider.sh (Linux/Mac)

3. Localizar *source (src)* do projeto desejado para instalar o NuGet;

4. Executar comando para instalar NuGet privado e seguir instruções;

  > *dotnet add package [NomePacote] -s https://pkgs.dev.azure.com/[NomeOrganizacao]/_packaging/[NomeProjeto]/nuget/v3/index.json --interactive

## CI/CD

- Arquivo de configuração padrão: [azure-pipelines.yml](azure-pipelines.yml).

## README

- Incluir documentação padrão no arquivo [README.md](README.md).
- Após inclusão da documentação padrão, **excluir** este arquivo e TODAS as **classes** indentificadas como exemplo.
  - O serviço para busca de endereço **Via CEP** assim como o contexto de **Customer** foi utilizado apenas como exemplo. O uso do serviço **Via CEP** está disponível no *NuGet* corporativo.
