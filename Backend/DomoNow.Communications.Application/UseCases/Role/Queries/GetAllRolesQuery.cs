using DomoNow.Communications.Domain.Entities;
using MediatR;
using DomainRole = DomoNow.Communications.Domain.Entities.Role;

namespace DomoNow.Communications.Application.UseCases.Role.Queries
{
    public sealed record GetAllRolesQuery() : IRequest<IReadOnlyList<DomainRole>>;
}
