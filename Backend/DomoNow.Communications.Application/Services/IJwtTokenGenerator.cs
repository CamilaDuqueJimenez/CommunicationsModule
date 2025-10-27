using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
