using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.Announcement.Commands.Handlers
{
    internal sealed class ToggleAnnouncementHandler(IAnnouncementRepository announcementRepository, ICurrentUserService currentUserService) : IRequestHandler<ToggleAnnouncementCommand, bool>
    {
        private readonly IAnnouncementRepository _announcementRepository = announcementRepository ?? throw new ArgumentNullException(nameof(announcementRepository));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        public async Task<bool> Handle(ToggleAnnouncementCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated) throw new UnauthorizedAccessException();
            var entity = await _announcementRepository.GetById(request.Id) ?? throw new KeyNotFoundException("Announcement not found");


            entity.ToggleActive(request.IsActive, _currentUserService.UserId);
            await _announcementRepository.Update(entity);
            return true;

        }
    }
}
