using FluentValidation.Results;
using ValueOf;
using Wiz.Template.Domain.Validations;

namespace Wiz.Template.Domain.ValueObjects
{

    public class Celsius : ValueOf<int, Celsius>
    {
        public bool IsValid { get; private set; }
        public ValidationResult ValidationResult { get; private set; } = new ValidationResult();

        protected override void Validate()
        {
            var validator = new CelsiusValidation();

            ValidationResult = validator.Validate(this);
            IsValid = ValidationResult.IsValid;
        }
    }
}
