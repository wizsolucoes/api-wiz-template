using System.Text.Json.Serialization;

namespace Wiz.Template.Domain.Models.Services
{
    public class ViaCEP
    {
        public ViaCEP() { }

        public ViaCEP(string cep, string street, string streetFull, string uf)
        {
            CEP = cep;
            Street = street;
            StreetFull = streetFull;
            UF = uf;
        }

        [JsonPropertyName("cep")]
        public string CEP { get; set; }
        [JsonPropertyName("logradouro")]
        public string Street { get; set; }
        [JsonPropertyName("complemento")]
        public string StreetFull { get; set; }
        [JsonPropertyName("uf")]
        public string UF { get; set; }
    }
}
