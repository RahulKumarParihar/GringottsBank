using BankLibrary.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class CreateAccountValidator : ValidationRules<CreateAccountDto>
    {
        public CreateAccountValidator(CreateAccountDto businessObject) : base(businessObject)
        {
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateRequired("CustomerId");
            ValidateGreaterThanZero("CustomerId");

            await Task.FromResult(default(CancellationToken));

            return this.ValidationResult;
        }
    }
}
