using BankLibrary.RequestParameters;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class TransactionParameterValidator : ParameterValidator
    {
        public TransactionParameterValidator(TransactionParameter businessObject) : base(businessObject)
        {
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateRequired("AccountId");
            ValidateGreaterThanZero("AccountId");

            return await base.ValidateAsync();
        }
    }
}
