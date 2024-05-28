using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ModelBindingAndValidationExcercise.Models;

namespace ModelBindingAndValidationExcercise.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Invoice Price should be equal to the total cost of all products (i.e {0}) in the order";
        public InvoicePriceValidatorAttribute()
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo? OtherProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (OtherProperty != null)
                {
                    List<Product> products = (List<Product>)OtherProperty.GetValue(validationContext.ObjectInstance)!;

                    double totalPrice = 0;
                    foreach (Product product in products)
                    {
                        totalPrice = product.Price * product.Quantity;
                    }
                    double actualPrice = (double)value;

                    if (totalPrice > 0)
                    {
                        if (totalPrice != actualPrice)
                        {
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, totalPrice), new string[] { nameof(validationContext.MemberName) });
                        }
                    }
                    else
                    {
                        return new ValidationResult("No products found to validate invoice price", new string[] { nameof(validationContext.MemberName) });
                    }
                    return ValidationResult.Success;
                }
                return null;
            }
            else
                return null;
        }
    }
}
