using MediatR;

namespace DomoNow.Communications.Application.UseCases.Role.Commands
{
    public sealed record CreateRoleCommand(string Name, string? Description) : IRequest<Guid>;
}
