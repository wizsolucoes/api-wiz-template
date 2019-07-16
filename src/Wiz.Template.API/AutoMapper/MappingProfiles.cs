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

            CreateMap<CustomerAddress, CustomerAddressViewModel>();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();

            #endregion
        }
    }
}
