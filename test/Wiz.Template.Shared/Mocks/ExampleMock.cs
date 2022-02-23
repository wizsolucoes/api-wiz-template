using System;
using Bogus;
using Wiz.Template.API.ViewModels.Exemple;
using Wiz.Template.Domain.Entities;
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
    }
}
