using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Models;

namespace Wiz.Template.Domain.Validation.CustomerValidation
{
    public class CustomerInsertValidation : AbstractValidator<Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerInsertValidation(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(x => x.AddressId)
                .NotNull()
                .WithMessage("Endereço não pode ser nulo");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Nome não pode ser nulo");

            RuleFor(x => x)
                .MustAsync(ValidationName)
                .WithMessage("Nome já cadastrado na base de dados");
        }

        private async Task<bool> ValidationName(Customer customer, CancellationToken cancellationToken)
        {
            var customerRepository = await _customerRepository.GetByNameAsync(customer.Name);

            return customer.Name != customerRepository?.Name;
        }
    }
}
