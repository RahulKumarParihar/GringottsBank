using BankLibrary.RequestParameters;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class CustomerTransactionValidator : ParameterValidator
    {
        public CustomerTransactionValidator(CustomerTransactionParameter businessObject) : base(businessObject)
        {
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateRequired("CustomerID");
            ValidateGreaterThanZero("CustomerID");

            return await base.ValidateAsync();
        }
    }
}
