using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.ViewModels.Customer;

namespace Wiz.Template.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerAddressViewModel>> GetAllAsync();
        Task<CustomerViewModel> GetByIdAsync(CustomerIdViewModel customerVM);
        Task<CustomerAddressViewModel> GetAddressByIdAsync(CustomerIdViewModel customerVM);
        Task<CustomerAddressViewModel> GetAddressByNameAsync(CustomerNameViewModel customerVM);
        CustomerViewModel Add(CustomerViewModel customerVM);
        void Update(CustomerViewModel customerVM);
        void Remove(CustomerViewModel customerVM);
    }
}
