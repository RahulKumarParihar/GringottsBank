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

            var result = new Success<AccountDto>
            {
                Data = await accountService.AddAccount(mapper.Map<AccountDto>(account))
            };

            return result;
        }

        public async Task<ResponseModel<AccountDto>> CloseAccount(int id)
        {
            var parameterValidation = new IdentityValidator(id);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<AccountDto>(validationResult);
            }

            var existingAccount = await accountService.GetAccount(id);
            
            var closeAccountValidator = new CloseAccountValidator(existingAccount);
            validationResult = await closeAccountValidator.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<AccountDto>(validationResult);
            }
            existingAccount.CloseDate = System.DateTime.Now;

            var result = new Success<AccountDto>
            {
                Data = await accountService.UpdateAccount(existingAccount)
            };

            return result;
        }
    }
}
