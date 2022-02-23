using FluentValidation;
using Wiz.Template.Domain.ValueObjects;

namespace Wiz.Template.Domain.Validations
{
    public class CelsiusValidation : AbstractValidator<Celsius>
    {
        public CelsiusValidation()
        {
            RuleFor(x => x.Value)
                .InclusiveBetween(-100, 100)
                .WithMessage("Temperature must be between -100.00C and 100.00C");
        }
    }
}
