using MediatR;

namespace DomoNow.Communications.Application.UseCases.Auth.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<string>;
}
