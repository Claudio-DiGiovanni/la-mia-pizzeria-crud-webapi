using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Attributes
{
    public class GreaterThanZero : ValidationAttribute
    {
        int valoreDiComparazione;
        protected override ValidationResult IsValid(object? value, ValidationContext _)
        {
            var input = Convert.ToDouble(value);

            if (input <= valoreDiComparazione)
            {
                return new ValidationResult(ErrorMessage ?? $"Il campo deve esserer maggiore di {valoreDiComparazione}");
            }

            return ValidationResult.Success!;
        }
        public GreaterThanZero(int n)
        {
            valoreDiComparazione = n;
        }
    }
}
