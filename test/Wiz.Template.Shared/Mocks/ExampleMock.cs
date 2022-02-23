using System;
using Bogus;
using Wiz.Template.API.ViewModels.Exemple;

namespace Wiz.Template.Shared.Mocks
{
    public static class ExampleMock
    {
        public static Faker<RequestExampleViewModel> RequestExampleViewModelFaker =>
            new Faker<RequestExampleViewModel>()
                .RuleFor(x => x.Date, () => new DateTime(2022, 2, 2));
    }
}
