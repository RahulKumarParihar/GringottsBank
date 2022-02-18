using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;

namespace BankLibrary.Abstracts
{
    public interface ICustomerService
    {
        PagedResponse<CustomerDto> GetCustomer(int id);
        PagedResponse<CustomerDto> GetCustomers(Parameters customerParameters);
    }
}
