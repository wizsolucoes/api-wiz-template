using FluentValidation;
using Wiz.Template.Domain.Models;

namespace Wiz.Template.Domain.Validation.CustomerValidation
{
    public class CustomerDeleteValidation : AbstractValidator<Customer>
    {
        public CustomerDeleteValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id não pode ser nulo");
        }
    }
}
