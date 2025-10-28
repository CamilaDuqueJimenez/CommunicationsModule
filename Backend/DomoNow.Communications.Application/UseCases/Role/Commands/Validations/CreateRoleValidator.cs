using FluentValidation;

namespace DomoNow.Communications.Application.UseCases.Role.Commands.Validations
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(300);
        }
    }
}
