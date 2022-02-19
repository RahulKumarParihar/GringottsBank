using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankLibrary.Validations
{
    public class ValidationRules<T>
    {
        private T businessObject;
        public virtual Task<ValidationResult> ValidateAsync()
        {
            return Task.FromResult(default(ValidationResult));
        }
        protected T BusinessObject { set { businessObject = value; } }
        public ValidationResult ValidationResult { get; } = new ValidationResult();

        protected ValidationRules(T businessObject)
        {
            this.BusinessObject = businessObject;
        }

        protected object GetPropertyValue(string propName)
        {
            return businessObject.GetType().GetProperty(propName).GetValue(businessObject, null);
        }

        protected void AddValidationError(string propName, string errorMessage)
        {
            if (!ValidationResult.Errors.ContainsKey(propName))
            {
                ValidationResult.Errors.Add(propName, new List<string>());
            }
            ValidationResult.Errors[propName].Add((errorMessage));

        }

        protected void AddValidationError(string propName, string errorMessage, int index)
        {

            string propertyNameWithIndex = $"{propName}/{index}";

            AddValidationError(propertyNameWithIndex, errorMessage);
        }

        protected void AddValidationWarning(string propName, string warningMessage)
        {
            if (!ValidationResult.Warnings.ContainsKey(propName))
            {
                ValidationResult.Warnings.Add(propName, new List<string>());
            }
            ValidationResult.Warnings[propName].Add((warningMessage));
        }

        protected void AddValidationWarning(string propName, string warningMessage, int index)
        {

            string propertyNameWithIndex = $"{propName}/{index}";

            AddValidationWarning(propertyNameWithIndex, warningMessage);
        }

        protected bool ValidateRequired(string propName)
        {
            object valueOf = GetPropertyValue(propName);
            if (!Validation.ValidateRequired(valueOf))
            {
                AddValidationError(propName, $"Required");
                return false;
            }
            return true;
        }

        protected void ValidationError(string propertyName, string errorMessage)
        {
            AddValidationError(propertyName, errorMessage);
        }

        protected bool ValidateLength(string propertyName, int maxLength)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.ValidateMaxLength(valueOf, maxLength) == false)
            {
                string errorMessage = $"Must be less than {maxLength} characters";
                AddValidationError(propertyName, errorMessage);
                return false;
            }

            return true;
        }

        protected bool ValidateMinLength(string propertyName, int minLength)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.ValidateMinLength(valueOf, minLength) == false)
            {
                string errorMessage = $"Must be more than {minLength} characters";
                AddValidationError(propertyName, errorMessage);
                return false;
            }

            return true;
        }

        protected bool ValidateMatchString(string propertyName1, string propertyName2)
        {
            object valueOf1 = GetPropertyValue(propertyName1);
            object valueOf2 = GetPropertyValue(propertyName2);
            if (Validation.ValidateMatchString(valueOf1, valueOf2) == false)
            {
                string errorMessage = "Does not match " + propertyName2;
                AddValidationError(propertyName1, errorMessage);
                return false;
            }

            return true;
        }

        protected bool ValidateNumeric(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.IsInteger(valueOf) == false)
            {
                AddValidationError(propertyName, "Is not a valid number");
                return false;
            }

            return true;
        }

        protected bool ValidateGreaterThanZero(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.ValidateGreaterThanZero(valueOf) == false)
            {
                AddValidationError(propertyName, "Must be greater than zero");
                return false;
            }

            return true;
        }

        protected bool ValidateDecimalGreaterThanZero(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.ValidateDecimalGreaterThanZero(valueOf) == false)
            {
                AddValidationError(propertyName, "Must be greater than zero");
                return false;
            }

            return true;
        }

        protected bool ValidateDecimalIsNotZero(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.ValidateDecimalIsNotZero(valueOf) == false)
            {
                AddValidationError(propertyName, "Must not equal zero");
                return false;
            }

            return true;
        }
        protected bool ValidateSelectedValue(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.ValidateGreaterThanZero(valueOf) == false)
            {
                AddValidationError(propertyName, "Not selected");
                return false;
            }

            return true;
        }

        protected bool ValidateSelectedValue(string propertyName, int index)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.ValidateGreaterThanZero(valueOf) == false)
            {
                AddValidationError(propertyName, "Not selected", index);
                return false;
            }

            return true;
        }

        protected bool ValidateIsDate(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.IsDate(valueOf) == false)
            {
                AddValidationError(propertyName, "Not a valid date");
                return false;
            }

            return true;
        }

        protected bool ValidateIsDateOrNullDate(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.IsDateOrNullDate(valueOf) == false)
            {
                AddValidationError(propertyName, "Not a valid date");
                return false;
            }

            return true;
        }

        protected bool ValidateRequiredDate(string propertyName)
        {
            object valueOf = GetPropertyValue(propertyName);
            if (Validation.IsDateGreaterThanDefaultDate(valueOf) == false)
            {
                AddValidationError(propertyName, "Required");
                return false;
            }

            return true;
        }

        protected bool ValidateEndDateBeforeStartDate(string startPropertyName, string endPropertyName)
        {
            object startValueOf = GetPropertyValue(startPropertyName);
            object endValueOf = GetPropertyValue(endPropertyName);
            if (Validation.IsEndDateBeforeStartDate(startValueOf, endValueOf) == false)
            {
                AddValidationError(endPropertyName, $"Must be greater than {startPropertyName}");
                return false;
            }

            return true;
        }

        protected bool ValidateNullObject(object value, string objectName)
        {
            if (value == null)
            {
                AddValidationError(objectName, "Must not be null");
            }
            return true;
        }

        protected bool ValidateNotNullObject(object value, string objectName)
        {
            if (value != null)
            {
                AddValidationError(objectName, "Must be null");
            }
            return true;
        }

        protected bool ValidateIdentityGreaterThanZero()
        {
            if (Validation.ValidateGreaterThanZero(businessObject) == false)
            {
                AddValidationError("Identity", "Must not equal zero");
            }
            return true;
        }
    }

    public class ValidationResult
    {
        public bool ValidationStatus { get { return !Errors.Any();  } }
        public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Warnings = new Dictionary<string, List<string>>();
    }
}
