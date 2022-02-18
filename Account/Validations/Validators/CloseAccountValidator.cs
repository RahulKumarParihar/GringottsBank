using BankLibrary.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class CloseAccountValidator : ValidationRules<AccountDto>
    {
        private readonly AccountDto businessObject;

        public CloseAccountValidator(AccountDto businessObject) : base(businessObject)
        {
            this.businessObject = businessObject;
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateNotNullObject(this.businessObject.CloseDate, "CloseDate");

            await Task.FromResult(default(CancellationToken));

            return this.ValidationResult;
        }
    }
}
