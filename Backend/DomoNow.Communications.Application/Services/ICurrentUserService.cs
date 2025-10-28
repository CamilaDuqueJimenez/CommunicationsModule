namespace DomoNow.Communications.Application.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        Guid? TowerId { get; }
        Guid? ApartmentId { get; }
        Guid RoleId { get; }
        bool IsAuthenticated { get; }
    }
}
