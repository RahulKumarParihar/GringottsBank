using System.Threading;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class IdentityValidator : ValidationRules<int>
    {
        public IdentityValidator(int businessObject) : base(businessObject)
        {

        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateIdentityGreaterThanZero();

            await Task.FromResult(default(CancellationToken));

            return this.ValidationResult;
        }
    }
}
