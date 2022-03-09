using System;
using Bogus;
using Wiz.Template.API.ViewModels.CepViewModels;
using Wiz.Template.API.ViewModels.ExampleViewModels;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.ValueObjects;

namespace Wiz.Template.Shared.Mocks
{
    public static class ExampleMock
    {
        public static Faker<RequestExampleViewModel> RequestExampleViewModelFaker =>
            new Faker<RequestExampleViewModel>()
                .RuleFor(x => x.Date, () => new DateTime(2022, 2, 2));

        public static Faker<Example> ExampleFaker => new Faker<Example>()
            .RuleFor(x => x.Id, f => f.UniqueIndex)
            .RuleFor(x => x.Date, f => f.Date.Recent())
            .RuleFor(
                x => x.TemperatureC,
                f => Celsius.From(f.Random.Int(-100, 100))
            )
            .RuleFor(x => x.Summary, "Mild");

        public static Faker<RequestCreateExampleViewModel> RequestCreateExampleViewModelFaker =>
            new Faker<RequestCreateExampleViewModel>()
                .RuleFor(x => x.TemperatureC, f => f.Random.Int(-100, 100))
                .RuleFor(x => x.Summary, "Mild");

        public static Faker<RequestInformacaoCepViewModel> RequestInformacaoCepViewModelFaker =>
            new Faker<RequestInformacaoCepViewModel>()
                .RuleFor(x => x.Cep, "74491615");

        public static Faker<ViaCep> ViaCepFaker => new Faker<ViaCep>()
            .RuleFor(x => x.Cep, "74491615")
            .RuleFor(x => x.Logradouro, "SQN 210 Bloco E")
            .RuleFor(x => x.Complemento, "Apartamento")
            .RuleFor(x => x.Bairro, "Brasília")
            .RuleFor(x => x.Localidade, "DF")
            .RuleFor(x => x.Ibge, 5300108)
            .RuleFor(x => x.Gia, "")
            .RuleFor(x => x.DDD, 61)
            .RuleFor(x => x.Siafi, 9701)
            .RuleFor(x => x.Erro, false);
    }
}
