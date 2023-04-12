using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationL.Common.Validators
{
    public class DateOfBirthValidator : AbstractValidator<string>
    {
        public DateOfBirthValidator(string dateOfBirth)
        {
            RuleFor(dateOfBirth => dateOfBirth)
                .Must(dateOfBirth => DateTime.TryParse(dateOfBirth, new CultureInfo("sk-Sk"), out DateTime o)).WithMessage("Incorrect date format.");
        }

        public string DateOfBirthValidate(string dateOfBirth)
        {
            try
            {
                this.ValidateAndThrow(dateOfBirth);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return String.Empty;
        }
    }
}
