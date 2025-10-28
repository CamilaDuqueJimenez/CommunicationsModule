using DomoNow.Communications.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DomoNow.Communications.Infrastructure.Persistence.Services
{
    internal class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        private ClaimsPrincipal? Principal => _accessor.HttpContext?.User;
        public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;
        public Guid UserId => Guid.TryParse((Principal?.FindFirst(ClaimTypes.NameIdentifier) ?? Principal?.FindFirst("sub"))?.Value, out var id) ? id : Guid.Empty;
        public Guid? TowerId => Guid.TryParse(Principal?.FindFirst("towerId")?.Value, out var id) ? id : null;
        public Guid? ApartmentId => Guid.TryParse(Principal?.FindFirst("apartmentId")?.Value, out var id) ? id : null;
        public Guid RoleId => Guid.TryParse(Principal?.FindFirst("roleId")?.Value, out var id) ? id : Guid.Empty;
    }
}
