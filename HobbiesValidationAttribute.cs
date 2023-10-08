using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Login2
{
    public class HobbiesAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string[] hobbies = value as string[];

                if (hobbies != null && hobbies.Length == 1)
                {
                    return ValidationResult.Success; // Valid, only one hobby is selected
                }
            }

            return new ValidationResult(ErrorMessage ?? "Only one hobby can be selected.");
        }
    }

    public class PassworddAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return ValidationResult.Success; // Let the [Required] attribute handle empty values.
            }

            if (password.Length < 8 ||
                (password.Take(4).Count(char.IsLower) < 2 || password.Take(4).Count(char.IsUpper) < 1) ||
                (!password.Substring(4).Any(char.IsDigit) || !password.Substring(4).Any(char.IsSymbol)))
            {
                return new ValidationResult("The password must have at least 8 characters. The first 4 characters must have at least 1 upper case and 2 lower case letters. The next 4 characters must be a combination of special characters and numbers.");
            }

            return ValidationResult.Success;
        }
    }


    public class AIUBAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string aid = value as string;

            if (string.IsNullOrEmpty(aid))
            {
                return ValidationResult.Success; // Let the [Required] attribute handle empty values.
            }

            if (!Regex.IsMatch(aid, @"^[0-9]{2}-\d{5}-[0-9]{1}$"))
            {
                return new ValidationResult("Aid must be in the format XX-XXXXX-X.");
            }

            return ValidationResult.Success;
        }
    }

    public class EmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = value as string;

            if (string.IsNullOrEmpty(email))
            {
                return ValidationResult.Success; // Let the [Required] attribute handle empty values.
            }

            if (!Regex.IsMatch(email, @"^[0-9]{2}-\d{5}-[0-9]{1}@student.aiub.edu$"))
            {
                return new ValidationResult("Email must be in the format XX-XXXXX-X@student.aiub.edu.");
            }

            // Check if the first part of the email matches the Aid
            var aidProperty = validationContext.ObjectType.GetProperty("Aid");
            if (aidProperty != null)
            {
                var aidValue = aidProperty.GetValue(validationContext.ObjectInstance, null) as string;
                if (!email.StartsWith(aidValue))
                {
                    return new ValidationResult("The first part of the email must match with Aid.");
                }
            }

            return ValidationResult.Success;
        }
    }


    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                int age = DateTime.Today.Year - dateOfBirth.Year;

                if (DateTime.Today < dateOfBirth.AddYears(age))
                {
                    age--;
                }

                if (age < _minimumAge)
                {
                    return new ValidationResult($"Must be at least {_minimumAge} years old.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}