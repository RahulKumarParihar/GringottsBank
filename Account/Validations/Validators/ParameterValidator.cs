using BankLibrary.RequestParameters;
using System.Threading;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class ParameterValidator : ValidationRules<Parameters>
    {
        public ParameterValidator(Parameters businessObject) : base(businessObject)
        {
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateRequired("PageNumber");
            ValidateRequired("PageSize");
            ValidateGreaterThanZero("PageNumber");
            ValidateGreaterThanZero("PageSize");

            await Task.FromResult(default(CancellationToken));

            return this.ValidationResult;
        }
    }
}
