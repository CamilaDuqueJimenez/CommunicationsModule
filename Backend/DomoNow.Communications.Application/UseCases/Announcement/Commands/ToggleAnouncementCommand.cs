using MediatR;

namespace DomoNow.Communications.Application.UseCases.Announcement.Commands
{
    public sealed record ToggleAnnouncementCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public bool IsActive { get; init; }
    }
}
