using BankLibrary.Data.Tables;

namespace BankLibrary.Validations.Validators
{
    public class CustomerValidator : ValidationRules<Customer>
    {
        protected CustomerValidator(Customer businessObject) : base(businessObject)
        {
        }
    }
}
