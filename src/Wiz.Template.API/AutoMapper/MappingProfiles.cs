using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;

namespace Wiz.Template.API.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Customer

            CreateMap<CustomerAddress, CustomerAddressViewModel>()
                .ConstructUsing(s => new CustomerAddressViewModel(
                    s.Id,
                    s.AddressId, 
                    s.Name, 
                    s.DateCreated, 
                    s.CEP, 
                    null));
            CreateMap<Customer, CustomerViewModel>()
                .ConstructUsing(s=> new CustomerViewModel(
                    s.Id,
                    s.AddressId,
                    s.Name
                )).ReverseMap();

            #endregion
        }
    }
}
