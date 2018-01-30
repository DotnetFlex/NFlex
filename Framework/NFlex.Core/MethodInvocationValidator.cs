using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace NFlex.Core
{
    public class MethodInvocationValidator
    {
        private MethodInfo _method;
        private object[] _arguments;
        private List<ValidationResult> _validationErrors=new List<ValidationResult>();
        public MethodInvocationValidator(MethodInfo method,object[] arguments)
        {
            _method = method;
            _arguments = arguments;
        }

        public void Validate()
        {
            if (!_method.IsPublic) return;
            if (_arguments == null || !_arguments.Any()) return;
            
            foreach (var arg in _arguments)
            {
                ValidateObjects(arg);
            }

            if(_validationErrors.Any())
            {
                throw new ValidationException(
                    "方法参数验证不通过！参见 ValidationErrors 属性获取详细信息",
                    _validationErrors
                    );
            }
        }

        private void ValidateObjects(object obj)
        {
            if (obj is IEnumerable && !(obj is IQueryable))
            {
                foreach (var item in (obj as IEnumerable))
                    ValidateObjects(item);
            }

            if (!(obj is IValidate)) return;

            ValidateObject(obj);

            var properties = TypeDescriptor.GetProperties(obj).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                ValidateObjects(property.GetValue(obj));
            }
        }

        private void ValidateObject(object obj)
        {
            var properties = TypeDescriptor.GetProperties(obj).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (!validationAttributes.Any()) continue;
                var validationContext = new ValidationContext(obj)
                {
                    DisplayName = property.Name,
                    MemberName = property.Name
                };
                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(obj), validationContext);
                    if (result != null)
                    {
                        _validationErrors.Add(result);
                    }
                }
            }
        }
    }
}
