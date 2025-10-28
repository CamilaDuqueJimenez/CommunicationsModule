using DomoNow.Communications.Application.Repositories;
using MediatR;
using DomainRole = DomoNow.Communications.Domain.Entities.Role;

namespace DomoNow.Communications.Application.UseCases.Role.Commands.Handlers
{
    internal sealed class CreateRoleHandler(IRoleRepository roleRepository) : IRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new DomainRole
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description ?? string.Empty
            };
            await _roleRepository.Create(role);
            return role.Id;
        }
    }
}
