using System;
using System.ComponentModel.DataAnnotations;

namespace NFlex.Core.ValidationAttributes
{
    public class RequiredDateTimeAttribute : ValidationAttribute
    {
        public RequiredDateTimeAttribute()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var guid = value.To<DateTime>();
            if (guid == DateTime.MinValue)
                return new ValidationResult(ErrorMessage);
            else
                return ValidationResult.Success;
        }
    }
}
