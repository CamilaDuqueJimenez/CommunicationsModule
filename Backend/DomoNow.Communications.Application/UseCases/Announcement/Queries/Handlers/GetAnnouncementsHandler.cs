using DomoNow.Communications.Application.Models;
using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Application.UseCases.Announcement.Dtos;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.Announcement.Queries.Handlers
{
    internal sealed class GetAnnouncementsHandler(IAnnouncementRepository announcementRepository, ICurrentUserService currentUserService) : IRequestHandler<GetAnnouncementsQuery, PaginationResponse<AnnouncementListItemDto>>
    {
        private readonly IAnnouncementRepository _announcementRepository = announcementRepository ?? throw new ArgumentNullException(nameof(announcementRepository));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        public async Task<PaginationResponse<AnnouncementListItemDto>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated) throw new UnauthorizedAccessException();
            return await _announcementRepository.GetPagedForUser(_currentUserService.TowerId, _currentUserService.ApartmentId, request.queryParam, onlyActive: true);
        }
    }
}
