using FluentValidation;

namespace DomoNow.Communications.Application.UseCases.User.Commands.Validations
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.TowerId).NotEmpty();
            RuleFor(x => x.ApartmentId).NotEmpty();
        }
    }
}
