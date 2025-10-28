using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using MediatR;
using EntityType = DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.UseCases.User.Commands.Handlers
{
    internal sealed class CreateUserHandler(IAuthRepository userRepository, IPassword passwordService) : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IAuthRepository _userRepository = userRepository;
        private readonly IPassword _passwordService = passwordService;
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetByEmail(request.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("El correo ya está registrado");
            }
            var hashed = await _passwordService.Hash(request.Password);
            var user = new EntityType.User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                Password = hashed,
                RoleId = request.RoleId,
                TowerId = request.TowerId,
                ApartmentId = request.ApartmentId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            await _userRepository.Create(user);
            return user.Id;
        }
    }
}
