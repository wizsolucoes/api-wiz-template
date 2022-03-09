using Wiz.Template.Domain.DTOs;
using Wiz.Template.Domain.ValueObjects;

namespace Wiz.Template.Domain.Entities
{
    public class Example : Entity
    {
        public DateTime Date { get; private set; }
        public Celsius TemperatureC { get; private set; }
        public int TemperatureF { get => 32 + (int)(TemperatureC.Value / 0.5556); }
        public string? Summary { get; private set; }

        public Example() : base(default)
        {
            TemperatureC = new Celsius();
        }

        private Example(ExampleDto dto) : base(dto.Id)
        {
            Date = dto.Date;
            TemperatureC = Celsius.From(dto.TemperatureC);
            Summary = dto.Summary;
        }

        private Example(int temperatureC, string summary) : base(default)
        {
            Date = DateTime.Now;
            TemperatureC = Celsius.From(temperatureC);
            Summary = summary;
        }

        public static Example From(ExampleDto dto)
        {
            return new Example(dto);
        }

        public static Example From(int temperatureC, string summary)
        {
            return new Example(temperatureC, summary);
        }
    }
}
