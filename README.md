# Wiz - API

- [Roadmap de melhorias](#roadmap-de-melhorias)
- [Desenvolvimento, por onde começar](#desenvolvimento-por-onde-começar)
- [Execução do projeto](#execução-do-projeto)
- [Estrutura](#estrutura)
- [Build e testes](#build-e-testes)
- [NuGet privado](#nuget-privado)
- [CI/CD](#ci/cd)
- [README](#readme)

## Roadmap de melhorias

- [x] Incluir exemplos de como usar o [MediatR](https://github.com/jbogard/MediatR) no nosso template.
- [ ] Incluir um exemplo de repository sem precisar implementar a interface do Dapper.
- [ ] Criar artigo explicando o beneficio de usar o mapeamento (Code first).
- [ ] Criar artigo mostrando um benchmark comparando o [Dapper.SimpleCRUD](https://github.com/ericdc1/Dapper.SimpleCRUD) com o Entity Framework Core.
- [ ] Criar um exemplo usando a extensão do Dapper utilizada em algumas squads da Wiz.
- [ ] Criar pasta para incluir tutoriais e instruções para utilizar o template.
- [ ] Remover Entity migration do template.
- [ ] Adicionar um artigo de como adicionar o Entity migration na API.
- [ ] Alterar a nomenclatura das classes Service que estão no projeto Wiz.Template.Infra para HttpService.
- [x] Padronizar a nomenclatura da ViewModel para Request...ViewModel e Response...ViewModel.
- [ ] Adicionar no template exemplo de como trabalhar com cache distribuído (ex: redis).
- [ ] Atualizar os arquivos dockers para o dotnet 6.
- [x] Atualizar as ViewModel para incluir exemplo de como adicionar uma descrição no Swagger.
- [ ] Adicionar exemplo de como trabalhar com processamento assíncrono.
- [ ] Criar artigo mostrando como trabalhar com Azure Service Bus.
- [ ] Criar artigo mostrando como trabalhar com Envio de email.
- [ ] Criar artigo mostrando como trabalhar com geração de pdf.
- [ ] Criar artigo mostrando como trabalhar com logs da aplicação.
- [ ] Toda classe do template que for somente para auxiliar o desenvolvedor com exemplos, deve começar com o nome Example. Indicando que pode ser excluída do projeto com segurança antes de enviar para homologação e produção.

## Desenvolvimento, por onde começar

TODO

## Execução do projeto

TODO

## Estrutura

Padrão das camadas do projeto:

1. **Wiz.[NomeProjeto].API**: responsável pela camada de *disponibilização* dos endpoints da API;
2. **Wiz.[NomeProjeto].Domain**: domínio da aplicação, responsável de manter as *regras de negócio* para a API;
3. **Wiz.[NomeProjeto].Infra**: camada mais baixa, para acesso a dados, infraestrutura e serviços externos;
4. **Wiz.[NomeProjeto].Contract.Tests**: responsável pela camada de *testes de contrato* dos projetos.
5. **Wiz.[NomeProjeto].Integration.Tests**: responsável pela camada de *testes de integração* dos projetos.
6. **Wiz.[NomeProjeto].Shared**: responsável por receber objetos mocks, builders e fixtures para ajudar na criação dos testes.
7. **Wiz.[NomeProjeto].Unit.Tests**: responsável pela camada de *testes unitários* dos projetos.

Formatação do projeto dentro do repositório:

```console
├── src
  ├── Wiz.[NomeProjeto].API (projeto)
  ├── Wiz.[NomeProjeto].Domain (projeto)
  ├── Wiz.[NomeProjeto].Infra (projeto)
├── test
  ├── Wiz.[NomeProjeto].Contract.Tests (projeto)
  ├── Wiz.[NomeProjeto].Integration.Tests (projeto)
  ├── Wiz.[NomeProjeto].Shared (projeto)
  ├── Wiz.[NomeProjeto].Unit.Tests (projeto)
├── Wiz.[NomeProjeto] (solução)
```

## Build e testes

### **Build com Visual Studio**

1. Comandos para geração de build:

- Debug: Executar via Test Explorer (adicionar breakpoint)
- Release: Executar via Test Explorer (não adicionar breakpoint)

2. Ativar funcionalidade [Live Unit Testing](https://docs.microsoft.com/pt-br/visualstudio/test/live-unit-testing?view=vs-2022) para executar testes em tempo de desenvolvimento (execução) do projeto.

3. Ativar funcionalidade [Code Coverage](https://docs.microsoft.com/en-us/visualstudio/test/using-code-coverage-to-determine-how-much-code-is-being-tested?view=vs-2022) para cobertura de testes.

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

Nossas APIs são provisionadas usando o Azure Devops, portanto as informações nescessarias para para liberar uma versão em homologação ou produção estão concentrada no arquivo [azure-pipelines.yml](azure-pipelines.yml).

## README

- Incluir documentação padrão no arquivo [README.md](README.md).
- Após inclusão da documentação padrão, **excluir** este arquivo e TODAS as **classes** indentificadas como exemplo.
  - O serviço para busca de endereço **Via CEP** assim como o contexto de **Customer** foi utilizado apenas como exemplo. O uso do serviço **Via CEP** está disponível no *NuGet* corporativo.
