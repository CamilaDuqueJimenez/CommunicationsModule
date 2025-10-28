using MediatR;

namespace DomoNow.Communications.Application.UseCases.ReadConfirmation.Commands
{
    public sealed record ConfirmReadCommand : IRequest<bool>
    {
        public Guid AnnouncementId { get; init; }
    }
}
