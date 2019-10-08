using System.Runtime.Serialization;

namespace Wiz.Template.Domain.Models.Services
{
    [DataContract(Name = "endereco")]
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

        [DataMember(Name = "cep")]
        public string CEP { get; set; }
        [DataMember(Name = "logradouro")]
        public string Street { get; set; }
        [DataMember(Name = "complemento")]
        public string StreetFull { get; set; }
        [DataMember(Name = "uf")]
        public string UF { get; set; }
    }
}
