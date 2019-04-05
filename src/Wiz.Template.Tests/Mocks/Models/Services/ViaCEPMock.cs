using Bogus;
using Wiz.Template.Domain.Models.Services;

namespace Wiz.Template.Tests.Mocks.Models.Services
{
    public class ViaCEPMock
    {
        private readonly static Faker _faker = new Faker();

        public static ViaCEP GetCEP()
        {
            return new ViaCEP(
                cep: _faker.Address.ZipCode(),
                street: _faker.Address.StreetName(),
                streetFull: _faker.Address.StreetAddress(),
                uf: _faker.Address.CountryCode(Bogus.DataSets.Iso3166Format.Alpha2)
            );
        }
    }
}
