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
  > *dotnet new -i [Path]*    
    *dotnet new wizapi -n [NomeProjeto]*

4. Executar comando para configurar aplicação em modo **(HTTPS)**;
  > *dotnet dev-certs https --trust*

5. Incluir configurações de *varíaveis de ambiente* no caminho abaixo:

### **Visual Studio**

```
├── Wiz.[NomeProjeto] (solução)
  ├── Wiz.[NomeProjeto].API (projeto)
    ├── appsettings.{ENVIRONMENT}.json
```

### **Visual Studio Code**

```
├── src (pasta física)
  ├── Wiz.[NomeProjeto].API (projeto)
    ├── appsettings.{ENVIRONMENT}.json
```

Dentro do arquivo *local.settings.json*, há o conteúdo para modificação das variáveis:

```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "KEY_APPLICATION_INSIGHTS"
  },
  "ConnectionStrings": {
    "CustomerDB": "CONNECTION_DATABASE"
  },
  "TokenValidationParameters": {
    "ValidAudience": "URL_SSO"
  },
  "API": {
    "ViaCEP": "https://viacep.com.br/ws/"
  },
  "HealthChecks-UI": {
    "HealthChecks": [
      {
        "Name": "Customer DB Health",
        "Uri": "https://localhost:5001/health"
      }
    ],
    "Webhooks": [],
    "EvaluationTimeOnSeconds": 30,
    "MinimumSecondsBetweenFailureNotifications": 60
  }
}
}
```

6. *(Opcional)* Inserir chave do **Application Insights** conforme configurado no Azure no arquivo *appsettings.{ENVIRONMENT}.json*.

Caso não há chave de configuração no Azure, não é necessário inserir para executar o projeto local.

## Execução do projeto

### **Visual Studio**

1. Executar projeto via **Kestrel**;

Executar o projeto via **Kestrel** facilita a troca de ambientes *(environments)* e a verificação de logs em execução da aplicação em projetos .NET Core. Os ambientes podem ser configurados dentro das propriedades do projeto, conforme caminho abaixo:

```
├── Wiz.[NomeProjeto] (solução)
  ├── Wiz.[NomeProjeto].API (projeto)
    ├── Properties (pasta física)
      ├── launchSettings.json
```

Dentro do arquivo *launchSettings.json*, há o conteúdo que indica a configuração de ambiente via **Kestrel**:

```json
    "Wiz.[NomeProjeto].API": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    }
```

### **Visual Studio Code**

1. *(Recomendado)* Instalar extensões para desenvolvimento:
  + [ASP.NET core VS Code Extension Pack](https://marketplace.visualstudio.com/items?itemName=temilaj.asp-net-core-vs-code-extension-pack)
  + [Azure Functions](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions)
  + [GitLens — Git supercharged](https://marketplace.visualstudio.com/items?itemName=eamodio.gitlens)
  + [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager)
  + [vscode-icons](https://marketplace.visualstudio.com/items?itemName=vscode-icons-team.vscode-icons)

2. *(Recomendado)* Instalar extensões para testes:
  + [.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)
  + [Coverage Gutters](https://marketplace.visualstudio.com/items?itemName=ryanluker.vscode-coverage-gutters)

3. Executar projeto via **Kestrel** *(Tecla F5)*;

Por padrão, todo projeto executado no **Visual Studio Code** é executado via **Kestrel** *(Tecla F5)*. Os ambientes podem ser configurados dentro das propriedades do projeto, conforme caminho abaixo:

```
├── .vscode (pasta física)
  ├── launch.json
```

4. Utilizar a função **task** para executar ações dentro do projeto. A função está presente no caminho do *menu* abaixo:

```
Terminal -> Run Task
```

5. Selecionar a função **task** a ser executada no projeto:
  + *clean* - Limpar solução 
  + *restore* - Restaurar pacotes da solução
  + *build* - Compilar pacotes da solução
  + *test* - Executar projeto de testes
  + *test with coverage* - Executar projeto de testes com cobertura

## Estrutura

Padrão das camadas do projeto:

1. **Wiz.[NomeProjeto].Domain**: domínio da aplicação, responsável de manter as *regras de negócio* para a API;
2. **Wiz.[NomeProjeto].Infra**: camada mais baixa, para acesso a dados, infraestrutura e serviços externos;
3. **Wiz.[NomeProjeto].API**: responsável pela camada de *disponibilização* dos endpoints da API;
4. **Wiz.[NomeProjeto].Tests**: responsável pela camada de *testes unitários e de integração* dos projetos.

Formatação do projeto dentro do repositório:

```
├── src 
  ├── Wiz.[NomeProjeto].Domain (projeto)
  ├── Wiz.[NomeProjeto].Infra (projeto)
  ├── Wiz.[NomeProjeto].API (projeto)
  ├── Wiz.[NomeProjeto].Tests (projeto)
├── Wiz.[NomeProjeto] (solução)
```

## Dependências

* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2)
* [Patterns RESTful](http://standards.rest/) 

## Build e testes

* Obrigatoriedade de **80%** de teste de cobertura.

### **Visual Studio**

1. Comandos para geração de build:
  + Debug: Executar via Test Explorer (adicionar breakpoint)
  + Release: Executar via Test Explorer (não adicionar breakpoint)

2. Ativar funcionalidade [Live Unit Testing](https://docs.microsoft.com/en-us/visualstudio/test/live-unit-testing?view=vs-2017) para executar testes em tempo de desenvolvimento (execução) do projeto.

3. Ativar funcionalidade [Code Coverage](https://docs.microsoft.com/en-us/visualstudio/test/using-code-coverage-to-determine-how-much-code-is-being-tested?view=vs-2017) para cobertura de testes.

As funcionalidades **Live Unit Testing** e **Code Coverage** estão disponíveis apenas na versão **Enterprise** do Visual Studio.

### **Visual Studio Code**

1. Executar **task** de teste desejada:
  + *test* - Executar projeto de testes
  + *test with coverage* - Executar projeto de testes com cobertura

2. Ativar **Watch** na parte inferior do Visual Studio Code para habilitar cores nas classes que descrevem a cobertura. É necessário executar os testes no modo *test with coverage*.

## NuGet privado

### **Visual Studio**

1. Adicionar *url* do NuGet privado no caminho do *menu* abaixo:

```
Tools -> NuGet Package Manager -> Package Sources
```

### **Visual Studio Code**

1. Abrir *Prompt de Comando* de sua preferência (**CMD** ou **PowerShell**) ou utilizar o terminal do Visual Studio Code;

1. Localizar *source (src)* do projeto desejado para instalar o NuGet;

2. Executar comando para instalar NuGet privado;
  > *dotnet add package [NomePacote] -s https://pkgs.dev.azure.com/[NomeOrganizacao]/_packaging/[NomeProjeto]/nuget/v3/index.json --interactive*

3. *(Opcional)* Na primeira instalação do NuGet privado irá aparecer no terminal um site com um código a ser inserido. Posteriormente, realizar login na conta desejada e o pacote já será instalado.

A instalação é realizada apenas uma vez até a expiração do token de autenticação (3 meses).

## CI/CD

* Arquivo de configuração padrão: [azure-pipelines.yml](azure-pipelines.yml).
* Caso há necessidade de incluir mais *tasks* ao pipeline, verfique a documentação para inclusão: [Azure DevOps - Yaml Schema](https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema).

## README

* Incluir documentação padrão no arquivo [README.md](README.md).
* Após inclusão da documentação padrão, **excluir** este arquivo e TODAS as **classes** indentificadas como exemplo.
  + O serviço para busca de endereço **Via CEP** assim como o contexto de **Customer** foi utilizado apenas como exemplo. O uso do serviço **Via CEP** está disponível no *NuGet* corporativo.