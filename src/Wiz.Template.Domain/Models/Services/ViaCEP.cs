using System.Text.Json.Serialization;
using Newtonsoft.Json;

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

        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [JsonProperty("complemento")]
        public string StreetFull { get; set; }
        [JsonProperty("uf")]
        public string UF { get; set; }
    }
}
