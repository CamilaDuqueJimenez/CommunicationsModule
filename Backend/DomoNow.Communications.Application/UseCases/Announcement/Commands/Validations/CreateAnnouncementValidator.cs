using DomoNow.Communications.Domain.Enums;
using FluentValidation;

namespace DomoNow.Communications.Application.UseCases.Announcement.Commands.Validations
{
    public class CreateAnnouncementValidator : AbstractValidator<CreateAnnouncementCommand>
    {
        public CreateAnnouncementValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.TargetScope).IsInEnum();
            When(x => x.TargetScope == TargetScope.Tower, () =>
            {
                RuleFor(x => x.TowerId).NotEmpty();
            });
            When(x => x.TargetScope == TargetScope.Apartment, () =>
            {
                RuleFor(x => x.ApartmentId).NotEmpty();
            });
        }
    }
}
