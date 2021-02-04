using Bogus;
using Bogus.Extensions.Brazil;
using Wiz.Template.Domain.Models.Services;

namespace Wiz.Template.Core.Tests.Mocks
{
    public static class ViaCEPMock
    {
        public static Faker<ViaCEP> ViaCEPModelFaker =>
            new Faker<ViaCEP>("pt_BR")
            .CustomInstantiator(x => new ViaCEP
            (
                cep: x.Address.ZipCode(),
                street: x.Address.StreetName(),
                streetFull: x.Address.StreetAddress(),
                uf: x.Address.CountryCode(Bogus.DataSets.Iso3166Format.Alpha2)
            ));
    }
}
