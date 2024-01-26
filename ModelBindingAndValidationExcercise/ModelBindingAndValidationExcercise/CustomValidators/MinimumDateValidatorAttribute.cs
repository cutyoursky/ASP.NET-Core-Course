using System.ComponentModel.DataAnnotations;

namespace ModelBindingAndValidationExcercise.CustomValidators
{
    public class MinimumDateValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; }
        public DateTime MinimumDate { get; set; }
        public MinimumDateValidatorAttribute(string minimumDateString)
        {
            MinimumDate = Convert.ToDateTime(minimumDateString);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null) 
            { 
                DateTime orderDate = (DateTime)value;
                if(orderDate > MinimumDate)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumDate.ToString("yyyy-MM-dd")), new string[] { nameof(validationContext.MemberName) });
                }

                return ValidationResult.Success;
            }
            return null;
        }
    }
}
