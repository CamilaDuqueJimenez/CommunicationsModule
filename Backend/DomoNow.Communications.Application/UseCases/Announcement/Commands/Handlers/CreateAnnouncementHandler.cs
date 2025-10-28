using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using MediatR;
using EntityType = DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.UseCases.Announcement.Commands.Handlers
{
    internal sealed class CreateAnnouncementHandler(IAnnouncementRepository announcementRepository, IAuthRepository authRepository, ICurrentUserService currentUserService) : IRequestHandler<CreateAnnouncementCommand, Guid>
    {
        private readonly IAnnouncementRepository _announcementRepository = announcementRepository ?? throw new ArgumentNullException(nameof(announcementRepository));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        private readonly IAuthRepository _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
        public async Task<Guid> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var user = await _authRepository.GetById(userId);
            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("User is not authorized to perform this action.");

            var entity = EntityType.Announcement.Create(
                 request.Title.Trim(),
                 request.Description.Trim(),
                 request.TargetScope,
                 request.TowerId,
                 request.ApartmentId,
                 userId
             );
            await _announcementRepository.Create(entity);
            return entity.Id;
        }
    }
}
