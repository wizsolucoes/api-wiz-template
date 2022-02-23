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
            //
        }

        private Example(ExampleDTO dto) : base(dto.Id)
        {
            Date = dto.Date;
            TemperatureC = Celsius.From(dto.TemperatureC);
            Summary = dto.Summary;
        }

        public static Example From(ExampleDTO dto)
        {
            return new Example(dto);
        }
    }
}
