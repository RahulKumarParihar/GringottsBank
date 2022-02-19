using AutoMapper;
using BankLibrary.Data.Tables;
using BankLibrary.DTOs;

namespace BankLibrary.Mappers
{
    public class CustomerMapper: Profile
    {
        public CustomerMapper()
        {
            CreateMap<Customer, CustomerDto>()
                    .ForMember(dest => dest.DisplayName, opts => opts.Ignore());

            CreateMap<CustomerDto, Customer>();

            CreateMap<CreateCustomerDto, Customer>();
        }

    }
}
