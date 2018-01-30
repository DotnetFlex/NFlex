using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NFlex.Core
{
    public class ValidationException:Exception
    {
        public List<ValidationResult> ValidationErrors { get; set; }

        public ValidationException(string message,List<ValidationResult> errors):base(message)
        {
            ValidationErrors = errors;
        }
    }
}
