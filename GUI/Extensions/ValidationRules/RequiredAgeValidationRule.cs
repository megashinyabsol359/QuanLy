using BUS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GUI.Extensions.ValidationRules
{
    public class RequiredAgeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime time;
            BUS_Config _busConfig = new BUS_Config();
            int _maxAge = _busConfig.GetMaxAge();
            int _minAge = _busConfig.GetMinAge();
            if (!DateTime.TryParse((value ?? "").ToString(),
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out time)) return new ValidationResult(false, "Invalid date");

            return DateTime.Now.Year - time.Year < _minAge || DateTime.Now.Year - time.Year > _maxAge
                ? new ValidationResult(false, $"Độ tuổi hợp lệ từ {_minAge} đến {_maxAge}")
                : ValidationResult.ValidResult;
        }
    }
}
