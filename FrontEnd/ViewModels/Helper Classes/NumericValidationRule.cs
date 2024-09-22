using System.Globalization;
using System.Windows.Controls;

namespace DataLogger.ViewModels
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null && int.TryParse(value.ToString(), out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Invalid number");
        }
    }
}
