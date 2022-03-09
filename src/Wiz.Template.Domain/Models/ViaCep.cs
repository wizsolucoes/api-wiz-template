namespace Wiz.Template.Domain.Models
{
    public record ViaCep
    {
        public string Cep { get; init; }
        public string Logradouro { get; init; }
        public string Complemento { get; init; }
        public string Bairro { get; init; }
        public string Localidade { get; init; }
        public string UF { get; init; }
        public int Ibge { get; init; }
        public string Gia { get; init; }
        public int DDD { get; init; }
        public int Siafi { get; init; }
        public bool Erro { get; init; }
    }
}
