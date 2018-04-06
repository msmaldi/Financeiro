using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Msmaldi.Financeiro.Website.DataAnnotations
{
    public class LessThanAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public LessThanAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(_propertyName);
            if (propertyTestedInfo == null)
                return new ValidationResult(string.Format("Unknown property {0}", _propertyName));

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || !(value is DateTime))
                return ValidationResult.Success;

            if (propertyTestedValue == null || !(propertyTestedValue is DateTime))
                return ValidationResult.Success;

            // Compare values
            if ((DateTime)value < (DateTime)propertyTestedValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public override string FormatErrorMessage(string name) {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _propertyName);
        }
    }
}