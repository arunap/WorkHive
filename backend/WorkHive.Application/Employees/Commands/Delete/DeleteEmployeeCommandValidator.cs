using FluentValidation;

namespace WorkHive.Application.Employees.Commands.Delete
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage("Id is required.");
        }
    }
}