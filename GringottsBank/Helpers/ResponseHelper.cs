using GringottsBank.Models;

namespace GringottsBank.Helpers
{
    public sealed class ResponseHelper
    {
        public static Error<T> AddValidationErrorToErrorResponse<T>(BankLibrary.Validations.ValidationResult validationResult)
        {
            var error = new Error<T>();
            foreach (var err in validationResult.Errors)
            {
                error.ErrorMessages.Add($"{err.Key}: {string.Join(',', err.Value)}.");
            }

            return error;
        }
    }
}
