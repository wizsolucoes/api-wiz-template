namespace Wiz.Template.API.ViewModels.CepViewModels
{
    public record ResponseInformacaoCepViewModel
    {
        /// <summary>
        /// Logradouro do endereço.
        /// </summary>
        /// <example>SQN 210 Bloco E</example>
        public string Logradouro { get; init; }
        /// <summary>
        /// Complemento do endereço.
        /// </summary>
        /// <example>Apartameto</example>
        public string Complemento { get; init; }
        /// <summary>
        /// Bairro do endereço.
        /// </summary>
        /// <example>Asa Norte</example>
        public string Bairro { get; init; }
        /// <summary>
        /// Cidade do endereço.
        /// </summary>
        /// <example>Brasília</example>
        public string Localidade { get; init; }
        /// <summary>
        /// Unidade Federativa do endereço.
        /// </summary>
        /// <example>DF</example>
        public string UF { get; init; }
        /// <summary>
        /// Código do IBGE para a localidade do endereço.
        /// </summary>
        /// <example>5300108</example>
        public int Ibge { get; init; }
        /// <summary>
        /// Guia de Informação e Apuração do ICMS
        /// </summary>
        /// <example>TBD</example>
        public string Gia { get; init; }
        /// <summary>
        /// Discagem Direta à Distância do endereço.
        /// </summary>
        /// <example>61</example>
        public int DDD { get; init; }
        /// <summary>
        /// Código do Sistema Integrado de Administração Financeira do Governo Federal.
        /// </summary>
        /// <example>9701</example>
        public int Siafi { get; init; }
    }
}
