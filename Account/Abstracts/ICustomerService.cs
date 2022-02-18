using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;
using System.Threading.Tasks;

namespace BankLibrary.Abstracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomer(int id);
        PagedResponse<CustomerDto> GetCustomers(Parameters customerParameters);
    }
}
