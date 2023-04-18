using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Attributes
{
    public class MoreThanFiveWords : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext _)
        {
            var input = value as string;

            if (input is null || input.Trim().Split(' ').Length < 5)
            {
                return new ValidationResult(ErrorMessage ?? "Il campo deve avere più di cinque parole");
            }

            return ValidationResult.Success!;
        }
    }
}
