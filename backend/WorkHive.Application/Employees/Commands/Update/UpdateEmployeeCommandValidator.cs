using FluentValidation;

namespace WorkHive.Application.Employees.Commands.Update
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Employee ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(6, 10).WithMessage("Name must be between 6 and 10 characters.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            // PhoneNumber validation
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[89]\d{7}$").WithMessage("Phone number must start with 8 or 9 and contain exactly 8 digits.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender is required and must be valid.");
        }
    }
}