using AutoMapper;
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
    public class AccountManager
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AccountManager(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<PagedResponse<AccountDto>>> GetAccounts(Parameters parameters)
        {
            var parameterValidation = new ParameterValidator(parameters);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<PagedResponse<AccountDto>>(validationResult);
            }

            var response = new Success<PagedResponse<AccountDto>>
            {
                Data = accountService.GetAccounts(parameters)
            };

            return response;
        }

        public async Task<ResponseModel<AccountDto>> GetAccount(int id)
        {
            var parameterValidation = new IdentityValidator(id);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<AccountDto>(validationResult);
            }

            var result = new Success<AccountDto>
            {
                Data = await accountService.GetAccount(id)
            };

            return result;
        }

        public async Task<ResponseModel<AccountDto>> AddAccount(CreateAccountDto account)
        {
            var parameterValidation = new CreateAccountValidator(account);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<AccountDto>(validationResult);
            }

            account.CreateDate = System.DateTime.Now;

            ResponseModel<AccountDto> result;
            try
            {
                result = new Success<AccountDto>
                {
                    Data = await accountService.AddAccount(mapper.Map<AccountDto>(account))
                };
            }
            catch (System.Exception ex)
            {
                result = ResponseHelper.CreateErrorResponseFromException<AccountDto>(ex);
            }

            return result;
        }

        public async Task<ResponseModel<AccountDto>> CloseAccount(int id)
        {
            ResponseModel<AccountDto> result;

            var validationErrorResponse = new Error<AccountDto>();
            var parameterValidation = new IdentityValidator(id);
            var existingValidationResult = await parameterValidation.ValidateAsync();
            ResponseHelper.AppendValidationErrorToErrorResponse(validationErrorResponse, existingValidationResult);

            var existingAccount = await accountService.GetAccount(id);

            var closeAccountValidator = new CloseAccountValidator(existingAccount);
            var validationResult = await closeAccountValidator.ValidateAsync();
            ResponseHelper.AppendValidationErrorToErrorResponse(validationErrorResponse, validationResult);

            if (!existingValidationResult.ValidationStatus || !validationResult.ValidationStatus)
                return validationErrorResponse;

            existingAccount.CloseDate = System.DateTime.Now;

            try
            {
                result = new Success<AccountDto>
                {
                    Data = await accountService.UpdateAccount(existingAccount)
                };
            }
            catch (System.Exception ex)
            {
                result = ResponseHelper.CreateErrorResponseFromException<AccountDto>(ex);
            }

            return result;
        }
    }
}
