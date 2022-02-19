using BankLibrary.RequestParameters;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class DateValidator : CustomerTransactionValidator
    {
        private readonly CustomerTransactionParameter businessObject;

        public DateValidator(CustomerTransactionParameter businessObject) : base(businessObject)
        {
            this.businessObject = businessObject;
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateRequiredDate("StartDate");
            if(businessObject.EndDate != null)
            {
                ValidateRequiredDate("EndDate");
            }
            ValidateEndDateBeforeStartDate("StartDate", "EndDate");

            return await base.ValidateAsync();
        }
    }
}
