using MediatR;

namespace DomoNow.Communications.Application.UseCases.User.Commands
{
    public sealed record CreateUserCommand(string FullName, string Email, string Password, Guid RoleId, Guid TowerId, Guid ApartmentId) : IRequest<Guid>;
}
