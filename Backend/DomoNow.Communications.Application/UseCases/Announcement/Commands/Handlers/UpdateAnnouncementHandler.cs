using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.Announcement.Commands.Handlers
{
    internal sealed class UpdateAnnouncementHandler(IAnnouncementRepository announcementRepository, ICurrentUserService currentUserService, IAuthRepository authRepository) : IRequestHandler<UpdateAnnouncementCommand, bool>
    {
        private readonly IAnnouncementRepository _announcementRepository = announcementRepository ?? throw new ArgumentNullException(nameof(announcementRepository));
        private readonly IAuthRepository _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        public async Task<bool> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var user = await _authRepository.GetById(userId);
            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("User is not authorized to perform this action.");

            var entity = await _announcementRepository.GetById(request.Id) ?? throw new KeyNotFoundException("Announcement not found");

            entity.UpdateTarget(request.TargetScope, request.TowerId, request.ApartmentId);
            entity.UpdateContent(request.Title.Trim(), request.Description.Trim(), userId);

            await _announcementRepository.Update(entity);
            return true;
        }
    }
}
