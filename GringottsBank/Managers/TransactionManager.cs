using AutoMapper;
using BankLibrary.Abstracts;
using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;
using BankLibrary.Validations;
using BankLibrary.Validations.Validators;
using GringottsBank.Helpers;
using GringottsBank.Models;
using System.Threading.Tasks;

namespace GringottsBank.Managers
{
    public class TransactionManager
    {
        private readonly ITransactionService transaction;
        private readonly IMapper mapper;

        public TransactionManager(ITransactionService transaction, IMapper mapper)
        {
            this.transaction = transaction;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<PagedResponse<TransactionDto>>> GetTransactionForAccounts(TransactionParameter parameters)
        {
            var parameterValidation = new TransactionParameterValidator(parameters);
            var validationResult = await parameterValidation.ValidateAsync();

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<PagedResponse<TransactionDto>>(validationResult);
            }

            var response = new Success<PagedResponse<TransactionDto>>
            {
                Data = transaction.GetTransactions(parameters)
            };

            return response;
        }

        public async Task<ResponseModel<PagedResponse<TransactionDto>>> GetTransactionsForCustomer(CustomerTransactionParameter parameters)
        {
            var errorRespone = new Error<PagedResponse<TransactionDto>>();

            var parameterValidation = new CustomerTransactionValidator(parameters);
            var validationResult = await parameterValidation.ValidateAsync();
            bool validationStatus = validationResult.ValidationStatus;
            ResponseHelper.AppendValidationErrorToErrorResponse(errorRespone, validationResult);

            if(parameters.StartDate != null || parameters.EndDate != null)
            {
                var dateValidator = new DateValidator(parameters);
                var datevalidationResult = await dateValidator.ValidateAsync();
                ResponseHelper.AppendValidationErrorToErrorResponse(errorRespone, datevalidationResult);

                validationStatus = validationResult.ValidationStatus && datevalidationResult.ValidationStatus;
            }

            if (!validationStatus)
                return errorRespone;

            var response = new Success<PagedResponse<TransactionDto>>
            {
                Data = transaction.GetTransactionsForCustomer(parameters)
            };

            return response;
        }

        public async Task<ResponseModel<TransactionDto>> Credit(AddTransactionDto transactionDto)
        {
            var validationResult = await GetValidationResultForAddTransaction(transactionDto);

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<TransactionDto>(validationResult);
            }

            var newTransaction = mapper.Map<TransactionDto>(transactionDto);
            newTransaction.Type = "C";

            return await AddTransaction(newTransaction);
        }

        public async Task<ResponseModel<TransactionDto>> Debit(AddTransactionDto transactionDto)
        {
            var validationResult = await GetValidationResultForAddTransaction(transactionDto);

            if (!validationResult.ValidationStatus)
            {
                return ResponseHelper.AddValidationErrorToErrorResponse<TransactionDto>(validationResult);
            }

            var newTransaction = mapper.Map<TransactionDto>(transactionDto);
            newTransaction.Type = "D";

            return await AddTransaction(newTransaction);
        }

        private async Task<ResponseModel<TransactionDto>> AddTransaction(TransactionDto transactionDto)
        {
            ResponseModel<TransactionDto> response;
            try
            {
                response = new Success<TransactionDto>
                {
                    Data = await transaction.AddTransaction(transactionDto)
                };
            }
            catch (System.Exception ex)
            {
                response = ResponseHelper.CreateErrorResponseFromException<TransactionDto>(ex);
            }

            return response;
        }

        private async Task<ValidationResult> GetValidationResultForAddTransaction(AddTransactionDto transactionDto)
        {
            var parameterValidation = new AddTransactionValidator(transactionDto);
            return await parameterValidation.ValidateAsync();
        }
    }
}
