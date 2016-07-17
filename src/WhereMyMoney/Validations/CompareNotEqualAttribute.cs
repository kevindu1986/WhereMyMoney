using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WhereMyMoney.Validations
{
    public class CompareNotEqualAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "The value of {0} cannot be the same as the value of the {1}.";

        public string OtherProperty { get; private set; }

        public CompareNotEqualAttribute(string otherProperty) : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("Other Property is null or empty.");
            }

            OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
           
        }
    }
}
