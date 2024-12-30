using Domain.Entities;
using FluentValidation;

namespace ApplicationL.Common.Validators
{
	public class PersonValidator : AbstractValidator<Person>
	{
		public PersonValidator(Person person)
		{
			var minimumDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-100));
			var todayDay = DateOnly.FromDateTime(DateTime.Now);

			RuleFor(person => person.Name)
				.NotEmpty().WithMessage("No name");
			RuleFor(person => person.Surname)
				.NotEmpty().WithMessage("No surname");
			RuleFor(person => person.DateOfBirth)
				.NotEmpty().WithMessage("No date")
				.GreaterThanOrEqualTo(minimumDay).WithMessage("Date too early")
				.LessThanOrEqualTo(todayDay).WithMessage("Date too late");
		}

		public string PersonValidate(Person person)
		{
			try
			{
				this.ValidateAndThrow(person);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

			return String.Empty;
		}
	}
}
