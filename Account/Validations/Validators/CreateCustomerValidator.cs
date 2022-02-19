using BankLibrary.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace BankLibrary.Validations.Validators
{
    public class CreateCustomerValidator : ValidationRules<CreateCustomerDto>
    {
        public CreateCustomerValidator(CreateCustomerDto businessObject) : base(businessObject)
        {
        }

        public override async Task<ValidationResult> ValidateAsync()
        {
            ValidateRequired("FirstName");
            ValidateRequired("LastName"); 
            ValidateRequired("Gender");
            ValidateRequired("Address");
            ValidateRequired("DateOfBirth");
            ValidateLength("Gender",2);
            ValidateLength("Address", 201);
            ValidateLength("FirstName", 51);
            ValidateLength("LastName", 51);
            ValidateRequiredDate("DateOfBirth");
            ValidateDateLessThanToday("DateOfBirth");

            await Task.FromResult(default(CancellationToken));

            return this.ValidationResult;
        }
    }
}
