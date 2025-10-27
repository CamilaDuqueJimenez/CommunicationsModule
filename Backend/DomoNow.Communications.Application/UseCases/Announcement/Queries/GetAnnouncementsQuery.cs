using DomoNow.Communications.Application.Models;
using DomoNow.Communications.Application.UseCases.Announcement.Dtos;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.Announcement.Queries
{
    public sealed record GetAnnouncementsQuery : IRequest<PaginationResponse<AnnouncementListItemDto>>
    {
        public QueryParam queryParam { get; set; } = new();
    }
}
