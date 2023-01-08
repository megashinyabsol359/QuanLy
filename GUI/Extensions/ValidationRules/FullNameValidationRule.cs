using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GUI.Extensions.ValidationRules
{
    public class FullNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var _fullNameRegex = @"^[A-Za-zÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠ-ỹ]+(?:\s[A-Za-zÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠ-ỹ]+)+$";
            return !Regex.IsMatch((value ?? "").ToString(), _fullNameRegex, RegexOptions.IgnoreCase)
                ? new ValidationResult(false, "Ít nhất hai từ, chỉ bao gồm chữ cái")
                : ValidationResult.ValidResult;
        }
    }
}
