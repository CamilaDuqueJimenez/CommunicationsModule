using DomoNow.Communications.Application.Repositories;
using MediatR;
using DomainRole = DomoNow.Communications.Domain.Entities.Role;

namespace DomoNow.Communications.Application.UseCases.Role.Queries.Handlers
{
    internal sealed class GetAllRolesHandler(IRoleRepository roleRepository) : IRequestHandler<GetAllRolesQuery, IReadOnlyList<DomainRole>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        public async Task<IReadOnlyList<DomainRole>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAll();
        }
    }
}
