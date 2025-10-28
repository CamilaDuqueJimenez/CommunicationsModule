using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using MediatR;
using EntityType = DomoNow.Communications.Domain.Entities.ReadConfirmation;

namespace DomoNow.Communications.Application.UseCases.ReadConfirmation.Commands.Handlers
{
    internal sealed class ConfirmReadHandler(IAnnouncementRepository announcementRepository, IReadConfirmationRepository readConfirmationRepository, ICurrentUserService currentUserService) : IRequestHandler<ConfirmReadCommand, bool>
    {
        private readonly IAnnouncementRepository _announcementRepository = announcementRepository ?? throw new ArgumentNullException(nameof(announcementRepository));
        private readonly IReadConfirmationRepository _readConfirmationRepository = readConfirmationRepository ?? throw new ArgumentNullException(nameof(readConfirmationRepository));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        public async Task<bool> Handle(ConfirmReadCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated) throw new UnauthorizedAccessException();
            var ann = await _announcementRepository.GetById(request.AnnouncementId) ?? throw new KeyNotFoundException("Anuncio no encontrado");

            if (!ann.IsActive) throw new InvalidOperationException("Anuncio inactivo");

            if (await _readConfirmationRepository.Exists(request.AnnouncementId, _currentUserService.UserId)) return true;

            var entity = EntityType.Create(request.AnnouncementId, _currentUserService.UserId, DateTime.UtcNow);
            await _readConfirmationRepository.Create(entity);
            return true;
        }
    }
}
