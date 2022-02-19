using BankLibrary.Abstracts;
using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;
using BankLibrary.Validations.Validators;
using GringottsBank.Helpers;
using GringottsBank.Models;
using System.Threading.Tasks;

namespace GringottsBank.Managers
{
    public class CustomerManager
    {
        private readonly ICustomerService _customerService;

        public CustomerManager(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ResponseModel<PagedResponse<CustomerDto>>> GetCustomers(Parameters parameters)
        {
            var parameterValidation = new ParameterValidator(parameters);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<PagedResponse<CustomerDto>>(validationResult);
            }

            var response = new Success<PagedResponse<CustomerDto>>
            {
                Data = _customerService.GetCustomers(parameters)
            };

            return response;
        }

        public async Task<ResponseModel<CustomerDto>> GetCustomer(int id)
        {
            var parameterValidation = new IdentityValidator(id);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<CustomerDto>(validationResult);
            }

            var result = new Success<CustomerDto>
            {
                Data = await _customerService.GetCustomer(id)
            };

            return result;
        }

        public async Task<ResponseModel<CustomerDto>> AddCustomer(CreateCustomerDto customerDto)
        {
            var parameterValidation = new CreateCustomerValidator(customerDto);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<CustomerDto>(validationResult);
            }

            var response = new Success<CustomerDto>
            {
                Data = await _customerService.AddCustomer(customerDto)
            };

            return response;
        }
    }
}
