using DomoNow.Communications.Domain.Enums;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.Announcement.Commands
{
    public sealed record CreateAnnouncementCommand : IRequest<Guid>
    {
        public string Title { get; set; } 
        public string Description { get; set; }
        public TargetScope TargetScope { get; set; }
        public Guid? TowerId { get; set; }
        public Guid? ApartmentId { get; set; }
    }

}
