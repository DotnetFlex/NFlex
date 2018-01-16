using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
