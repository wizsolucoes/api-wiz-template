namespace Wiz.Template.API.ViewModels.Exemple
{
    public class ResponseExempleViewModel
    {
        /// <summary>
        /// Data da verificação
        /// </summary>
        /// <example>2022-03-07T18:03:50.456Z</example>
        public DateTime Date { get; init; }

        /// <summary>
        /// Temperatura em Celsius
        /// </summary>
        /// <example>28</example>
        public int TemperatureC { get; init; }
        /// <summary>
        /// Temperatura em Fahrenheit
        /// </summary>
        /// <example>82</example>

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// Resumo da sensação do dia
        /// </summary>
        /// <example>Mild</example>

        public string? Summary { get; init; }
    }
}
