using BankLibrary.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class AddTransactionValidator : ValidationRules<AddTransactionDto>
    {
        public AddTransactionValidator(AddTransactionDto businessObject) : base(businessObject)
        {
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateRequired("Amount");
            ValidateRequired("AccountId");
            ValidateDecimalGreaterThanZero("Amount");
            ValidateGreaterThanZero("AccountId"); ;

            await Task.FromResult(default(CancellationToken));

            return this.ValidationResult;
        }
    }
}
