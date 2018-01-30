using System;
using System.ComponentModel.DataAnnotations;

namespace NFlex.Core.ValidationAttributes
{
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public RequiredGuidAttribute()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var guid = value.To<Guid>();
            if (guid == Guid.Empty)
                return new ValidationResult(ErrorMessage);
            else
                return ValidationResult.Success;
        }
    }
}
