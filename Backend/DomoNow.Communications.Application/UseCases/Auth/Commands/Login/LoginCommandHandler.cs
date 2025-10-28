using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using MediatR;

namespace DomoNow.Communications.Application.UseCases.Auth.Commands.Login
{
    internal sealed class LoginCommandHandler(IAuthRepository userRepository, IPassword passwordService, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommand, string>
    {
        private readonly IAuthRepository _userRepository = userRepository;
        private readonly IPassword _passwordService = passwordService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email) ?? throw new UnauthorizedAccessException("Credenciales inválidas");
            var valid = await _passwordService.Verify(request.Password, user.Password);
            if (!valid)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }
            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
